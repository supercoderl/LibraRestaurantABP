using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace LibraRestaurant.CatalogService.Products
{
    public class ProductCodeAlreadyExistsException : BusinessException
    {
        public ProductCodeAlreadyExistsException(string productCode)
            : base("CatalogService:000001", $"A product with code {productCode} has already exists!")
        {
            WithData("productCode", productCode);
        }
    }
}
