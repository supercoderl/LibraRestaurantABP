using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.BasketService.Services
{
    public class BasketProductUpdatedEto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
