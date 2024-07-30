using AutoMapper;
using LibraRestaurant.OrderingService.Domain.Orders;
using LibraRestaurant.OrderingService.OrderItems;
using LibraRestaurant.OrderingService.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AutoMapper;

namespace LibraRestaurant.OrderingService.Application
{
    public class OrderingServiceApplicationAutoMapperProfile : Profile
    {
        public OrderingServiceApplicationAutoMapperProfile()
        {
            CreateMap<Address, OrderAddressDto>();
            CreateMap<Buyer, BuyerDto>();

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItem, TopSellingDto>();

            CreateMap<Order, OrderDto>()
                .Ignore(q => q.Address)
                .Ignore(q => q.Items)
                .Ignore(q => q.Buyer);
        }
    }
}
