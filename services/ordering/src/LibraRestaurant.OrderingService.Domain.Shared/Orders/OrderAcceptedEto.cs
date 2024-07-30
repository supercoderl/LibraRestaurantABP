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
    [Serializable]
    [EventName("LibraRestaurant.Order.Accepted")]
    public class OrderAcceptedEto : EtoBase
    {
        public Guid OrderId { get; set; }
        public string PaymentStatus { get; set; }
        public BuyerEto Buyer { get; set; }
        public List<OrderItemEto> Items { get; set; }
    }
}
