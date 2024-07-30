using LibraRestaurant.OrderingService.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LibraRestaurant.OrderingService.Orders
{
    public class OrderDto : EntityDto<Guid>
    {
        public DateTime OrderDate { get; set; }
        public int OrderNo { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string PaymentMethod { get; set; }
        public BuyerDto Buyer { get; set; }
        public OrderAddressDto Address { get; set; } = new();
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
