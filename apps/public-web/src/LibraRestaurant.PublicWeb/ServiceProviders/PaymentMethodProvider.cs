using LibraRestaurant.PaymentService.PaymentMethods;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LibraRestaurant.PublicWeb.ServiceProviders
{
    public class PaymentMethodProvider : ITransientDependency
    {
        protected IPaymentMethodAppService PaymentMethodAppService { get; }

        private readonly PaymentMethodUiOptions _options;

        public PaymentMethodProvider(
            IPaymentMethodAppService paymentMethodAppService,
            IOptions<PaymentMethodUiOptions> options)
        {
            PaymentMethodAppService = paymentMethodAppService;
            _options = options.Value;
        }

        public async Task<List<PaymentMethodViewModel>> GetPaymentMethodsAsync()
        {
            var paymentMethods = await PaymentMethodAppService.GetListAsync();

            return paymentMethods.Select((pm, i) => new PaymentMethodViewModel
            {
                Name = pm.Name,
                IsDefault = i == 0,
                IconCss = _options.Icons.GetOrDefault(pm.Name) ?? _options.DefaultIcon
            }).ToList();
        }
    }

    public class PaymentMethodViewModel
    {
        public string Name { get; set; }
        public string IconCss { get; set; }
        public bool IsDefault { get; set; }
    }
}
