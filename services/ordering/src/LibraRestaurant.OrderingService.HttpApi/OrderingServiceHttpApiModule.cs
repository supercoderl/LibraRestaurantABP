using LibraRestaurant.OrderingService.Localization;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace LibraRestaurant.OrderingService
{
    [DependsOn(
        typeof(OrderingServiceApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class OrderingServiceHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(OrderingServiceHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<OrderingServiceResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
