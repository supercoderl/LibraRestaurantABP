using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace LibraRestaurant.PaymentService.PaymentRequests
{
    [EventName("LibraRestaurant.Payment.Completed")]
    public class PaymentRequestCompletedEto : EtoBase, IHasExtraProperties
    {
        public Guid PaymentRequestId { get; set; }
        public string OrderId { get; set; }
        public int OrderNo { get; set; }
        public string Currency { get; set; }
        public string? BuyerId { get; set; }
        public PaymentRequestState State { get; set; }
        public ICollection<PaymentRequestProductEto> Products { get; set; } = new List<PaymentRequestProductEto>();
        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}
