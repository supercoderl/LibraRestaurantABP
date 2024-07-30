using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace LibraRestaurant.PaymentService.PaymentRequests
{
    [EventName("LibraRestaurant.Payment.Completed")]
    public class PaymentCompletedEto : EtoBase
    {
        public PaymentRequestDto PaymentRequest { get; set; }
    }
}
