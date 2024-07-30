using LibraRestaurant.BasketService.Contracts.Services.Dtos;
using LibraRestaurant.BasketService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.BasketService.Services
{
    public interface IBasketAppService
    {
        Task<BasketDto> GetAsync(Guid? anonymousUserId);
        Task<BasketDto> AddProductAsync(AddProductDto input);
        Task<BasketDto> RemoveProductAsync(RemoveProductDto input);
    }
}
