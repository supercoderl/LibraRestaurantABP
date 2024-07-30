using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.BackgroundJobs.RabbitMQ;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;
using Microsoft.AspNetCore.DataProtection;
using LibraRestaurant.Shared.Hosting.AspNetCore;
using LibraRestaurant.AdministrationService.EntityFrameworkCore;

namespace LibraRestaurant.Shared.Hosting.Microservices
{
    [DependsOn(
    typeof(LibraRestaurantSharedHostingAspNetCoreModule),
    typeof(AbpBackgroundJobsRabbitMqModule),
    typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
    typeof(AbpEventBusRabbitMqModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AdministrationServiceEntityFrameworkCoreModule),
    typeof(AbpDistributedLockingModule)
    )]
    public class LibraRestaurantSharedHostingMicroservicesModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            var configuration = context.Services.GetConfiguration();

            Configure<AbpDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "LibraRestaurant:";
            });

            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
            context.Services
                .AddDataProtection()
                .PersistKeysToStackExchangeRedis(redis, "LibraRestaurant-Protection-Keys");

            context.Services.AddSingleton<IDistributedLockProvider>(sp =>
            {
                var connection = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
                return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
            });
        }
    }
}
