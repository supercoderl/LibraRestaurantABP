using LibraRestaurant.PaymentService.PaymentMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;

namespace LibraRestaurant.PaymentService.ClientProxies
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IPaymentMethodAppService), typeof(PaymentMethodClientProxy))]
    public partial class PaymentMethodClientProxy : ClientProxyBase<IPaymentMethodAppService>, IPaymentMethodAppService
    {
        public virtual async Task<List<PaymentMethodDto>> GetListAsync()
        {
            return await RequestAsync<List<PaymentMethodDto>>(nameof(GetListAsync));
        }
    }
}
