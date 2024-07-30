using LibraRestaurant.CatalogService.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;

namespace LibraRestaurant.CatalogService.ClientProxies
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IPublicProductAppService), typeof(PublicProductClientProxy))]
    public partial class PublicProductClientProxy : ClientProxyBase<IPublicProductAppService>, IPublicProductAppService
    {
        public virtual async Task<ListResultDto<ProductDto>> GetListAsync()
        {
            return await RequestAsync<ListResultDto<ProductDto>>(nameof(GetListAsync));
        }

        public virtual async Task<ProductDto> GetAsync(Guid id)
        {
            return await RequestAsync<ProductDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
            {
                { typeof(Guid), id }
            });
        }
    }
}
