using LibraRestaurant.BasketService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.BasketService.Contracts.Services.Dtos
{
    public class RemoveProductDto : IHasAnonymousId
    {
        public Guid ProductId { get; set; }

        public int? Count { get; set; }

        public Guid? AnonymousId { get; set; }
    }
}
