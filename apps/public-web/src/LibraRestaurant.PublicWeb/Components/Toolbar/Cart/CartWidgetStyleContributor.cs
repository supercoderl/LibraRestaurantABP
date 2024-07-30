using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace LibraRestaurant.PublicWeb.Components.Toolbar.Cart
{
    public class CartWidgetStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/components/cart/cart-widget.css");
        }
    }
}
