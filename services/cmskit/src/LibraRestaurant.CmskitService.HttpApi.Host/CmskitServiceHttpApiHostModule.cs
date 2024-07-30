using LibraRestaurant.CmskitService.DbMigrations;
using LibraRestaurant.CmskitService.EntityFrameworkCore;
using LibraRestaurant.Shared.Hosting.AspNetCore;
using LibraRestaurant.Shared.Hosting.Microservices;
using Microsoft.AspNetCore.Cors;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.CmsKit.Comments;
using Volo.CmsKit.Ratings;

namespace LibraRestaurant.CmskitService
{
    [DependsOn(
        typeof(LibraRestaurantSharedHostingMicroservicesModule),
        typeof(CmskitServiceApplicationModule),
        typeof(CmskitServiceHttpApiModule),
        typeof(CmskitServiceEntityFrameworkCoreModule),
        typeof(AbpIdentityHttpApiClientModule)
        )]
    public class CmskitServiceHttpApiHostModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            FeatureConfigurer.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            JwtBearerConfigurationHelper.Configure(context, "CmskitService");

            var configuration = context.Services.GetConfiguration();

            SwaggerConfigurationHelper.ConfigureWithOidc(
                context: context,
                authority: configuration["AuthServer:Authority"]!,
                scopes: ["CmskitService"],
                discoveryEndpoint: configuration["AuthServer:MetadataAddress"],
                apiTitle: "Cmskit Service API"
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

            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(CmskitServiceApplicationModule).Assembly, opts =>
                {
                    opts.RootPath = "cmskit";
                    opts.RemoteServiceName = "Cmskit";
                });
            });

            Configure<AbpAntiForgeryOptions>(options =>
            {
                options.AutoValidate = false; //TODO 
            });


            Configure<CmsKitCommentOptions>(options =>
            {
                options.EntityTypes.Add(new CommentEntityTypeDefinition("quote"));
            });

            Configure<CmsKitRatingOptions>(options =>
            {
                options.EntityTypes.Add(new RatingEntityTypeDefinition("quote"));
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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Cmskit Service API");
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
                .GetRequiredService<CmskitServiceDatabaseMigrationChecker>()
                .CheckAndApplyDatabaseMigrationsAsync();
        }
    }
}
