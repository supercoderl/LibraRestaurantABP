using LibraRestaurant.CatalogService.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LibraRestaurant.CatalogService.Products
{
    public interface IProductAppService : IApplicationService
    {
        Task<PagedResultDto<ProductDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input);

        Task<ListResultDto<ProductDto>> GetListAsync();

        Task<ProductDto> GetAsync(Guid id);

        Task<ProductDto> CreateAsync(CreateProductDto input);

        Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto input);

        Task DeleteAsync(Guid id);
    }
}
