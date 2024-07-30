using LibraRestaurant.OrderingService.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace LibraRestaurant.OrderingService.Orders
{
    [EventName("LibraRestaurant.Order.Cancelled")]
    public class OrderCancelledEto : EtoBase
    {
        public Guid PaymentRequestId { get; set; }
        public Guid OrderId { get; set; }
        public int OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public BuyerEto Buyer { get; set; }
        public List<OrderItemEto> Items { get; set; } = new();
    }
}
