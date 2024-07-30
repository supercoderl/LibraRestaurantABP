using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LibraRestaurant.OrderingService.Orders
{
    public class PaymentDto : EntityDto
    {
        public decimal RateOfPaymentMethod { get; set; }
        public string PaymentMethod { get; set; }
    }
}
