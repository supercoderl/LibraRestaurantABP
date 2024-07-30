using LibraRestaurant.BasketService.Contracts.Services.Dtos;
using LibraRestaurant.BasketService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;

namespace LibraRestaurant.PublicWeb.ClientProxies
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IBasketAppService), typeof(BasketClientProxy))]
    public partial class BasketClientProxy : ClientProxyBase<IBasketAppService>, IBasketAppService
    {
        public virtual async Task<BasketDto> GetAsync(Guid? anonymousUserId)
        {
            return await RequestAsync<BasketDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid?), anonymousUserId }
        });
        }

        public virtual async Task<BasketDto> AddProductAsync(AddProductDto input)
        {
            return await RequestAsync<BasketDto>(nameof(AddProductAsync), new ClientProxyRequestTypeValue
        {
            { typeof(AddProductDto), input }
        });
        }

        public virtual async Task<BasketDto> RemoveProductAsync(RemoveProductDto input)
        {
            return await RequestAsync<BasketDto>(nameof(RemoveProductAsync), new ClientProxyRequestTypeValue
        {
            { typeof(RemoveProductDto), input }
        });
        }
    }
}
