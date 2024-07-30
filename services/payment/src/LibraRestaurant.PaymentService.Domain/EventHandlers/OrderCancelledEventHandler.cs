using LibraRestaurant.OrderingService.Orders;
using LibraRestaurant.PaymentService.PaymentRequests;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace LibraRestaurant.PaymentService.EventHandlers
{
    public class OrderCancelledEventHandler : IDistributedEventHandler<OrderCancelledEto>, ITransientDependency
    {
        private readonly IPaymentRequestRepository _paymentRepository;

        public OrderCancelledEventHandler(IPaymentRequestRepository paymenRepository)
        {
            _paymentRepository = paymenRepository;
        }

        public async Task HandleEventAsync(OrderCancelledEto eventData)
        {
            var payment = await _paymentRepository.GetAsync(eventData.PaymentRequestId);
            Log.Information($"Cancelled the order: {payment.OrderId} payment:{payment.Id}");
        }
    }
}
