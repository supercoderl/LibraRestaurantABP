using LibraRestaurant.Shared.Hosting.AspNetCore;
using LibraRestaurant.Shared.Hosting.Gateways;
using Microsoft.AspNetCore.Rewrite;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace LibraRestaurant.WebPublicGateway
{
    [DependsOn(
        typeof(LibraRestaurantSharedHostingGatewaysModule)
    )]
    public class LibraRestaurantWebPublicGatewayModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            SwaggerConfigurationHelper.ConfigureWithOidc(
                context: context,
                authority: configuration["AuthServer:Authority"]!,
                scopes:
                [
                    /* Requested scopes for authorization code request and descriptions for swagger UI only */
                    "IdentityService",
                    "AdministrationService",
                    "CatalogService",
                    "BasketService",
                    "PaymentService",
                    "OrderingService",
                    "CmskitService"
                ],
                apiTitle: "Web Gateway API",
                discoveryEndpoint: configuration["AuthServer:MetadataAddress"]
            );
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
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwaggerUIWithYarp(context);
            app.UseAbpSerilogEnrichers();

            app.UseRewriter(new RewriteOptions()
                // Regex for "", "/" and "" (whitespace)
                .AddRedirect("^(|\\|\\s+)$", "/swagger"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapReverseProxy();
            });
        }
    }
}
