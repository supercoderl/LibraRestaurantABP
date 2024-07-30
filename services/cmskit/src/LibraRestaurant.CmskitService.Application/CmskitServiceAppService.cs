using LibraRestaurant.CmskitService.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace LibraRestaurant.CmskitService
{
    public abstract class CmskitServiceAppService : ApplicationService
    {
        protected CmskitServiceAppService()
        {
            LocalizationResource = typeof(CmskitServiceResource);
            ObjectMapperContext = typeof(CmskitServiceApplicationModule);
        }
    }
}
