﻿using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace LibraRestaurant.PaymentService
{
    [DependsOn(
            typeof(PaymentServiceApplicationContractsModule),
            typeof(AbpAspNetCoreMvcModule))]
    public class PaymentServiceHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(PaymentServiceHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<PaymentServiceResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
