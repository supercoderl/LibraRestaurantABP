using AutoMapper;
using LibraRestaurant.CatalogService.Grpc;
using LibraRestaurant.CatalogService.Products;

namespace LibraRestaurant.BasketService
{
    public class BasketServiceApplicationAutoMapperProfile : Profile
    {
        public BasketServiceApplicationAutoMapperProfile()
        {
            CreateMap<ProductEto, ProductDto>();
            CreateMap<ProductResponse, ProductDto>();
        }
    }
}
