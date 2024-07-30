using Volo.Abp.Modularity;
using Volo.Abp.Identity;

namespace LibraRestaurant.IdentityService
{
    [DependsOn(
        typeof(IdentityServiceApplicationContractsModule),
        typeof(AbpIdentityHttpApiModule)
    )]
    public class IdentityServiceHttpApiModule : AbpModule
    {
    }
}
