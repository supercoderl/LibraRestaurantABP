using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace LibraRestaurant.CatalogService
{
    [DependsOn(
        typeof(CatalogServiceDomainSharedModule),
        typeof(AbpObjectExtendingModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
    )]
    public class CatalogServiceApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            CatalogServiceDtoExtensions.Configure();
        }
    }
}
