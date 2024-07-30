using LibraRestaurant.PaymentService.PaymentMethods;
using LibraRestaurant.PaymentService.Paypal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PayPalCheckoutSdk.Core;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace LibraRestaurant.PaymentService
{
    [DependsOn(
           typeof(PaymentServiceDomainModule),
           typeof(PaymentServiceApplicationContractsModule),
           typeof(AbpDddApplicationModule),
           typeof(AbpAutoMapperModule)
       )]
    public class PaymentServiceApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<PaymentServiceApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<PaymentServiceApplicationModule>(validate: true);
            });

            Configure<PayPalOptions>(context.Services.GetConfiguration().GetSection("Payment:PayPal"));

            context.Services.AddTransient(provider =>
            {
                var options = provider?.GetService<IOptions<PayPalOptions>>()?.Value;

                if (string.IsNullOrWhiteSpace(options?.Environment) || options.Environment == PayPalConsts.Environment.Sandbox)
                {
                    return new PayPalHttpClient(new SandboxEnvironment(options?.ClientId, options?.Secret));
                }

                return new PayPalHttpClient(new LiveEnvironment(options.ClientId, options.Secret));
            });

            context.Services.AddTransient<PaymentMethodResolver>(provider => new PaymentMethodResolver(
                provider.GetServices<IPaymentMethod>(),
                provider.GetRequiredService<ILogger<PaymentMethodResolver>>()
            ));
        }
    }
}
