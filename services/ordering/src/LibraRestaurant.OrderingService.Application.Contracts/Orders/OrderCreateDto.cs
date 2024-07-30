using LibraRestaurant.OrderingService.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.OrderingService.Orders
{
    public class OrderCreateDto
    {
        public string PaymentMethod { get; set; }
        public OrderAddressDto Address { get; set; } = new();
        public List<OrderItemCreateDto> Products { get; set; } = new();
    }
}
