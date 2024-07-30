using LibraRestaurant.PaymentService.PaymentRequests;
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
    [ExposeServices(typeof(IPaymentRequestAppService), typeof(PaymentRequestClientProxy))]
    public partial class PaymentRequestClientProxy : ClientProxyBase<IPaymentRequestAppService>, IPaymentRequestAppService
    {
        public virtual async Task<PaymentRequestDto> CompleteAsync(string paymentMethod, PaymentRequestCompleteInputDto input)
        {
            return await RequestAsync<PaymentRequestDto>(nameof(CompleteAsync), new ClientProxyRequestTypeValue
            {
                { typeof(string), paymentMethod },
                { typeof(PaymentRequestCompleteInputDto), input }
            });
        }

        public virtual async Task<PaymentRequestDto> CreateAsync(PaymentRequestCreationDto input)
        {
            return await RequestAsync<PaymentRequestDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
            {
                { typeof(PaymentRequestCreationDto), input }
            });
        }

        public virtual async Task<bool> HandleWebhookAsync(string paymentMethod, string payload)
        {
            return await RequestAsync<bool>(nameof(HandleWebhookAsync), new ClientProxyRequestTypeValue
            {
                { typeof(string), paymentMethod },
                { typeof(string), payload }
            });
        }

        public virtual async Task<PaymentRequestStartResultDto> StartAsync(string paymentMethod, PaymentRequestStartDto input)
        {
            return await RequestAsync<PaymentRequestStartResultDto>(nameof(StartAsync), new ClientProxyRequestTypeValue
            {
                { typeof(string), paymentMethod },
                { typeof(PaymentRequestStartDto), input }
            });
        }
    }
}
