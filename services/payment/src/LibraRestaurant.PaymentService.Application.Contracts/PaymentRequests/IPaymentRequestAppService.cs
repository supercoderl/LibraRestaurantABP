using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace LibraRestaurant.PaymentService.PaymentRequests
{
    public interface IPaymentRequestAppService : IApplicationService
    {
        Task<PaymentRequestDto> CreateAsync(PaymentRequestCreationDto input);

        Task<PaymentRequestStartResultDto> StartAsync(string paymentMethod, PaymentRequestStartDto input);

        Task<PaymentRequestDto> CompleteAsync(string paymentMethod, PaymentRequestCompleteInputDto input);

        Task<bool> HandleWebhookAsync(string paymentMethod, string payload);
    }
}
