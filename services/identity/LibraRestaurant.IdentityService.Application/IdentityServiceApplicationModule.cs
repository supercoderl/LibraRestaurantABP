
using LibraRestaurant.IdentityService.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace LibraRestaurant.IdentityService
{
    [DependsOn(
            typeof(IdentityServiceDomainModule),
            typeof(IdentityServiceApplicationContractsModule),
            typeof(AbpIdentityApplicationModule),
            typeof(AbpBackgroundJobsModule)
        )]
    public class IdentityServiceApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddAutoMapperObjectMapper<IdentityServiceApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<IdentityServiceApplicationModule>(validate: true);
            });

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
