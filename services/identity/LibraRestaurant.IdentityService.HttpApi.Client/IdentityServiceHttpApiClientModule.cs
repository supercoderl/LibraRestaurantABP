using Volo.Abp.Modularity;
using Volo.Abp.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LibraRestaurant.IdentityService.HttpApi.Client
{
    [DependsOn(
           typeof(IdentityServiceApplicationContractsModule),
           typeof(AbpIdentityHttpApiClientModule)
       )]
    public class IdentityServiceHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(IdentityServiceApplicationContractsModule).Assembly,
                IdentityServiceRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}
