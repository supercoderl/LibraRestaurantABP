﻿using LibraRestaurant.CatalogService.Products;
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
    [ExposeServices(typeof(IProductAppService), typeof(ProductClientProxy))]
    public partial class ProductClientProxy : ClientProxyBase<IProductAppService>, IProductAppService
    {
        public virtual async Task<PagedResultDto<ProductDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            return await RequestAsync<PagedResultDto<ProductDto>>(nameof(GetListPagedAsync), new ClientProxyRequestTypeValue
            {
                { typeof(PagedAndSortedResultRequestDto), input }
            });
        }

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

        public virtual async Task<ProductDto> CreateAsync(CreateProductDto input)
        {
            return await RequestAsync<ProductDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
            {
                { typeof(CreateProductDto), input }
            });
        }

        public virtual async Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto input)
        {
            return await RequestAsync<ProductDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
            {
                { typeof(Guid), id },
                { typeof(UpdateProductDto), input }
            });
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
            {
                { typeof(Guid), id }
            });
        }
    }
}
