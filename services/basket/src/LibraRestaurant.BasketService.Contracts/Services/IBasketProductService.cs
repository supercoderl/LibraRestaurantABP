using JetBrains.Annotations;
using LibraRestaurant.CatalogService.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.BasketService.Services
{
    public interface IBasketProductService
    {
        [ItemNotNull]
        Task<ProductDto> GetAsync(Guid productId);
    }
}
