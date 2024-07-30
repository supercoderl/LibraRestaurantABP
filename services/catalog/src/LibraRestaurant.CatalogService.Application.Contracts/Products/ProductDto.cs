using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LibraRestaurant.CatalogService.Products
{
    public class ProductDto : AuditedEntityDto<Guid>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string ImageName { get; set; }

        public float Price { get; set; }

        public int StockCount { get; set; }
    }
}
