using LibraRestaurant.PaymentService.PaymentRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LibraRestaurant.PaymentService.PaymentMethods
{
    public interface IPaymentMethod : ITransientDependency
    {
        string Name { get; }

        public Task<PaymentRequestStartResultDto> StartAsync(PaymentRequest paymentRequest, PaymentRequestStartDto input);

        public Task<PaymentRequest> CompleteAsync(IPaymentRequestRepository paymentRequestRepository, string token);

        public Task HandleWebhookAsync(string payload);
    }
}
