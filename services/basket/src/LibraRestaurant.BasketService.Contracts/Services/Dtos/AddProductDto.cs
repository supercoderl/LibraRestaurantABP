using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.BasketService.Services
{
    public class AddProductDto : IHasAnonymousId
    {
        public Guid ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Count { get; set; } = 1;

        public Guid? AnonymousId { get; set; }
    }
}
