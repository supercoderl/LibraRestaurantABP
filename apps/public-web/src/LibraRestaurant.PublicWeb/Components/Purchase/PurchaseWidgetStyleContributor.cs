using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace LibraRestaurant.PublicWeb.Components.Purchase
{
    public class PurchaseWidgetStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/components/purchase/purchase-widget.css");
        }
    }
}
