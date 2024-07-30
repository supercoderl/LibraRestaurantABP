using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Localization;
using Volo.Abp.Application.Services;
using LibraRestaurant.AdministrationService.Localization;

namespace LibraRestaurant.AdministrationService
{
    /* Inherit your application services from this class.
        */
    public abstract class AdministrationServiceAppService : ApplicationService
    {
        protected AdministrationServiceAppService()
        {
            LocalizationResource = typeof(AdministrationServiceResource);
        }
    }
}
