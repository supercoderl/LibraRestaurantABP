using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace LibraRestaurant.PaymentService.PaymentMethods
{
    public interface IPaymentMethodAppService : IApplicationService
    {
        Task<List<PaymentMethodDto>> GetListAsync();
    }
}
