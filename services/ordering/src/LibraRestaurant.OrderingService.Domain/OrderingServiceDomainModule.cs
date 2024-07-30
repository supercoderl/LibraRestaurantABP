using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace LibraRestaurant.OrderingService.Domain
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(OrderingServiceDomainSharedModule)
    )]
    public class OrderingServiceDomainModule : AbpModule
    {

    }
}
