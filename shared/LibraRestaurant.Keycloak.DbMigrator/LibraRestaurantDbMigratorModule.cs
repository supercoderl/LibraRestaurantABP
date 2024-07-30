using LibraRestaurant.DbMigrator;
using LibraRestaurant.Shared.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;

namespace LibraRestaurant.DbMigrator
{
    [DependsOn(
        typeof(LibraRestaurantSharedHostingModule)
    )]
    public class LibraRestaurantDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<KeycloakClientOptions>(options =>
            {
                options.Url = configuration["Keycloak:url"] ?? throw new Exception("Environment resource not found!");
                options.AdminUserName = configuration["Keycloak:adminUsername"] ?? throw new Exception("Environment resource not found!");
                options.AdminPassword = configuration["Keycloak:adminPassword"] ?? throw new Exception("Environment resource not found!");
                options.RealmName = configuration["Keycloak:realmName"] ?? throw new Exception("Environment resource not found!");
            }
            );
        }
    }
}
