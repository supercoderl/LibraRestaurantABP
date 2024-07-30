using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace LibraRestaurant.PublicWeb
{
    [Dependency(ReplaceServices = true)]
    public class LibraRestaurantBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "LibraRestaurant";
    }
}
