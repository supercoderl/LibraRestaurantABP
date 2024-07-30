using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace LibraRestaurant.OrderingService
{
    [DependsOn(
        typeof(OrderingServiceDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
    )]
    public class OrderingServiceApplicationContractsModule : AbpModule
    {

    }
}
