using LibraRestaurant.Shared.Hosting.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using Volo.Abp;
using LibraRestaurant.Shared.Hosting.Microservices;
using Microsoft.AspNetCore.Cors;
using LibraRestaurant.IdentityService.DbMigrations;
using LibraRestaurant.IdentityService.EntityFrameworkCore;

namespace LibraRestaurant.IdentityService
{
    [DependsOn(
        typeof(LibraRestaurantSharedHostingMicroservicesModule),
        typeof(IdentityServiceHttpApiModule),
        typeof(IdentityServiceApplicationModule),
        typeof(IdentityServiceEntityFrameworkCoreModule)
    )]
    public class IdentityServiceHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            JwtBearerConfigurationHelper.Configure(context, "IdentityService");

            SwaggerConfigurationHelper.ConfigureWithOidc(
                context: context,
                authority: configuration["AuthServer:Authority"]!,
                scopes: ["IdentityService"],
                discoveryEndpoint: configuration["AuthServer:MetadataAddress"],
                apiTitle: "Identity Service API"
                );

            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]!
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.Trim().RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            // Keycloak handles the user creation that a user name can be multiple words
            Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters = null;
            });

            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ExposeIntegrationServices = true;
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCorrelationId();
            app.UseCors();
            app.UseAbpRequestLocalization();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAbpClaimsMap();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseAbpSwaggerWithCustomScriptUI(options =>
            {
                var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity Service API");
                options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            });
            app.UseAbpSerilogEnrichers();
            app.UseAuditing();
            app.UseUnitOfWork();
            app.UseConfiguredEndpoints();
        }

        public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
        {
            await context.ServiceProvider
                .GetRequiredService<IdentityServiceDatabaseMigrationChecker>()
                .CheckAndApplyDatabaseMigrationsAsync();
        }
    }
}