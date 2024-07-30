using AutoMapper;
using LibraRestaurant.PaymentService.PaymentRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraRestaurant.PaymentService
{
    public class PaymentServiceApplicationAutoMapperProfile : Profile
    {
        public PaymentServiceApplicationAutoMapperProfile()
        {
            CreateMap<PaymentRequest, PaymentRequestDto>();
            CreateMap<PaymentRequestProduct, PaymentRequestProductDto>();

            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
        }
    }
}
