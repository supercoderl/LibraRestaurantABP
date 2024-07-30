using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LibraRestaurant.OrderingService.Orders
{
    public class BuyerDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
