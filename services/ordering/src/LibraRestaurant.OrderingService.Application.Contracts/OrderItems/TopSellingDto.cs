using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LibraRestaurant.OrderingService.OrderItems
{
    public class TopSellingDto : EntityDto
    {
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public int Units { get; set; }
    }
}
