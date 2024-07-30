using LibraRestaurant.CatalogService.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace LibraRestaurant.CatalogService.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class CatalogServiceController : AbpControllerBase
    {
        protected CatalogServiceController()
        {
            LocalizationResource = typeof(CatalogServiceResource);
        }
    }
}
