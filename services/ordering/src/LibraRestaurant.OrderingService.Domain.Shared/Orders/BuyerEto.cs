using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace LibraRestaurant.OrderingService.Orders
{
    public class BuyerEto : EtoBase
    {
        public Guid? BuyerId { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerName { get; set; }
    }
}
