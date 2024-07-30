using LibraRestaurant.OrderingService.Application.Contracts.Orders;
using LibraRestaurant.OrderingService.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LibraRestaurant.OrderingService.Orders
{
    public class DashboardDto : EntityDto
    {
        public List<TopSellingDto> TopSellings { get; set; }
        public List<PaymentDto> Payments { get; set; }
        public List<OrderStatusDto> OrderStatusDto { get; set; }
    }
}
