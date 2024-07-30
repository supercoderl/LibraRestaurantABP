﻿using LibraRestaurant.Shared.Hosting.AspNetCore;
using Volo.Abp.Application;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp;
using LibraRestaurant.Shared.Hosting.Microservices;
using Microsoft.Extensions.Caching.Distributed;
using LibraRestaurant.BasketService.Entities;
using Microsoft.AspNetCore.Cors;
using LibraRestaurant.BasketService.Contracts;
using LibraRestaurant.CatalogService.Grpc;
using Volo.Abp.Http.Client;
using Volo.Abp.AutoMapper;
using Polly;

namespace LibraRestaurant.BasketService
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpHttpClientModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpCachingModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpDddDomainModule),
        typeof(BasketServiceContractsModule),
        typeof(LibraRestaurantSharedHostingMicroservicesModule)
    )]
    public class BasketServiceModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpHttpClientBuilderOptions>(options =>
            {
                options.ProxyClientBuildActions.Add((_, clientBuilder) =>
                {
                    clientBuilder.AddTransientHttpErrorPolicy(policyBuilder =>
                        policyBuilder.WaitAndRetryAsync(
                            3,
                            i => TimeSpan.FromSeconds(Math.Pow(2, i))
                        )
                    );
                });
            });
        }
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = hostingEnvironment.IsDevelopment();

            var configuration = context.Services.GetConfiguration();

            ConfigureAutoMapper();
            ConfigureAspNetCoreRouting();
            ConfigureGrpc(context);
            ConfigureDistributedCache();
            ConfigureVirtualFileSystem();
            ConfigureAuthentication(context, configuration);
            ConfigureSwagger(context, configuration);
            ConfigureAutoApiControllers();
        }

        private void ConfigureAspNetCoreRouting()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(BasketServiceModule).Assembly, opts =>
                {
                    opts.RootPath = "basket";
                    opts.RemoteServiceName = BasketServiceConstants.RemoteServiceName;
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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket Service API");
                options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            });
            app.UseAbpSerilogEnrichers();
            app.UseAuditing();
            app.UseUnitOfWork();
            app.UseConfiguredEndpoints();
        }

        private void ConfigureSwagger(ServiceConfigurationContext context, IConfiguration configuration)
        {
            SwaggerConfigurationHelper.ConfigureWithOidc(
                context: context,
                authority: configuration["AuthServer:Authority"]!,
                scopes: ["BasketService"],
                discoveryEndpoint: configuration["AuthServer:MetadataAddress"],
                apiTitle: "Basket Service API"
            );
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            JwtBearerConfigurationHelper.Configure(context, "BasketService");

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

            Configure<AbpAntiForgeryOptions>(options => { options.AutoValidate = false; });
        }

        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(BasketServiceModule).Assembly);
            });
        }

        private void ConfigureGrpc(ServiceConfigurationContext context)
        {
            // context.Services.AddHttpForwarderWithServiceDiscovery();
            context.Services.AddGrpcClient<ProductPublic.ProductPublicClient>((services, options) =>
            {
                options.Address = new Uri("http://_grpc.catalogService");
            });
        }

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<BasketServiceModule>();
            });
        }

        private void ConfigureDistributedCache()
        {
            Configure<AbpDistributedCacheOptions>(options =>
            {
                options.CacheConfigurators.Add(cacheName =>
                {
                    if (cacheName == CacheNameAttribute.GetCacheName(typeof(Basket)))
                    {
                        return new DistributedCacheEntryOptions
                        {
                            SlidingExpiration = TimeSpan.FromDays(7)
                        };
                    }

                    return null;
                });
            });
        }

        private void ConfigureVirtualFileSystem()
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BasketServiceModule>();
            });
        }
    }
}
