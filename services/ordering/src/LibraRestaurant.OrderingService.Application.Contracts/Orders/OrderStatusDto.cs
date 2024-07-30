using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LibraRestaurant.OrderingService.Application.Contracts.Orders
{
    public class OrderStatusDto : EntityDto
    {
        public int CountOfStatusOrder { get; set; }
        public string OrderStatus { get; set; }
        public decimal TotalIncome { get; set; }
    }
}
