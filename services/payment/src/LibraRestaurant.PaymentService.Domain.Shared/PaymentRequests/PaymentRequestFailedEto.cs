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
    public class PaymentRequestFailedEto : EtoBase, IHasExtraProperties
    {
        public Guid PaymentRequestId { get; set; }

        public string OrderId { get; set; }
        public int OrderNo { get; set; }
        public string FailReason { get; set; }
        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}
