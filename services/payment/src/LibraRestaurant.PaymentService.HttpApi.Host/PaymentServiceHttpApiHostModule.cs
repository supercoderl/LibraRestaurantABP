﻿using LibraRestaurant.PaymentService.DbMigrations;
using LibraRestaurant.PaymentService.EntityFrameworkCore;
using LibraRestaurant.Shared.Hosting.AspNetCore;
using LibraRestaurant.Shared.Hosting.Microservices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace LibraRestaurant.PaymentService
{
    [DependsOn(
        typeof(PaymentServiceApplicationModule),
        typeof(PaymentServiceEntityFrameworkCoreModule),
        typeof(PaymentServiceHttpApiModule),
        typeof(LibraRestaurantSharedHostingMicroservicesModule)
    )]
    public class PaymentServiceHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            JwtBearerConfigurationHelper.Configure(context, "PaymentService");

            SwaggerConfigurationHelper.ConfigureWithOidc(
                context: context,
                authority: configuration["AuthServer:Authority"]!,
                scopes: ["PaymentService"],
                flows: ["authorization_code"],
                discoveryEndpoint: configuration["AuthServer:MetadataAddress"],
                apiTitle: "Payment Service API"
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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Service API");
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
                .GetRequiredService<PaymentServiceDatabaseMigrationChecker>()
                .CheckAndApplyDatabaseMigrationsAsync();
        }
    }
}
