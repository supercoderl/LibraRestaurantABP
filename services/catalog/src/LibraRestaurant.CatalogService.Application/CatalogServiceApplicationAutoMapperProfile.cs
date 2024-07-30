using AutoMapper;
using LibraRestaurant.CatalogService.Products;
using LibraRestaurant.CatalogService.Grpc;

namespace LibraRestaurant.CatalogService
{
    public class CatalogServiceApplicationAutoMapperProfile : Profile
    {
        public CatalogServiceApplicationAutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductResponse>();
        }
    }
}
