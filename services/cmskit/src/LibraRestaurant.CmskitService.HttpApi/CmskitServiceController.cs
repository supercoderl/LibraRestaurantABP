using LibraRestaurant.CmskitService.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace LibraRestaurant.CmskitService
{
    public abstract class CmskitServiceController : AbpControllerBase
    {
        protected CmskitServiceController()
        {
            LocalizationResource = typeof(CmskitServiceResource);
        }
    }
}
