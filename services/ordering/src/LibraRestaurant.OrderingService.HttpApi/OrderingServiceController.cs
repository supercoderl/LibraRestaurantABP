using LibraRestaurant.OrderingService.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace LibraRestaurant.OrderingService
{
    public abstract class OrderingServiceController : AbpControllerBase
    {
        protected OrderingServiceController()
        {
            LocalizationResource = typeof(OrderingServiceResource);
        }
    }
}
