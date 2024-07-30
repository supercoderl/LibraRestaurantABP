using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace LibraRestaurant.CmskitService
{
    [DependsOn(
        typeof(CmskitServiceDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule),
        typeof(CmsKitApplicationContractsModule))]
    public class CmskitServiceApplicationContractsModule : AbpModule
    {

    }
}
