using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace LibraRestaurant.PaymentService
{
    public abstract class PaymentServiceAppService : ApplicationService
    {
        protected PaymentServiceAppService()
        {
            LocalizationResource = typeof(PaymentServiceResource);
            ObjectMapperContext = typeof(PaymentServiceApplicationModule);
        }
    }
}
