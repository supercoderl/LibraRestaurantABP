using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using Volo.Abp;
using LibraRestaurant.OrderingService.Application;
using LibraRestaurant.Shared.Hosting.Microservices;
using LibraRestaurant.OrderingService.EntityFrameworkCore;
using LibraRestaurant.Shared.Hosting.AspNetCore;
using Microsoft.AspNetCore.Cors;
using LibraRestaurant.OrderingService.DbMigrations;

namespace LibraRestaurant.OrderingService
{
    [DependsOn(
        typeof(OrderingServiceHttpApiModule),
        typeof(OrderingServiceApplicationModule),
        typeof(OrderingServiceEntityFrameworkCoreModule),
        typeof(LibraRestaurantSharedHostingMicroservicesModule)
    )]
    public class OrderingServiceHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            JwtBearerConfigurationHelper.Configure(context, "OrderingService");

            SwaggerConfigurationHelper.ConfigureWithOidc(
                context: context,
                authority: configuration["AuthServer:Authority"]!,
                scopes: ["OrderingService"],
                discoveryEndpoint: configuration["AuthServer:MetadataAddress"],
                apiTitle: "Ordering Service API"
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

            // TODO: Crate controller instead of auto-controller configuration
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(OrderingServiceApplicationModule).Assembly, opts =>
                {
                    opts.RootPath = "ordering";
                    opts.RemoteServiceName = "Ordering";
                });
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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering Service API");
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
                .GetRequiredService<OrderingServiceDatabaseMigrationChecker>()
                .CheckAndApplyDatabaseMigrationsAsync();
        }
    }
}
