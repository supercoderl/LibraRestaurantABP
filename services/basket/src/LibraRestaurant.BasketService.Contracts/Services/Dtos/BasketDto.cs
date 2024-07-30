using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.BasketService.Services
{
    public class BasketDto
    {
        public float TotalPrice { get; set; }
        public List<BasketItemDto> Items { get; set; }

        public BasketDto()
        {
            Items = new List<BasketItemDto>();
        }
    }
}
