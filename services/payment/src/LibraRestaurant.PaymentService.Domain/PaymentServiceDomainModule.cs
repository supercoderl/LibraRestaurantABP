using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace LibraRestaurant.PaymentService
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(PaymentServiceDomainSharedModule)
    )]
    public class PaymentServiceDomainModule : AbpModule
    {

    }
}
