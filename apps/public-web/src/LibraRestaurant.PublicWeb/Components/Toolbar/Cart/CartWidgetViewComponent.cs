using LibraRestaurant.PublicWeb.ServiceProviders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;
using Volo.Abp.AspNetCore.Mvc;

namespace LibraRestaurant.PublicWeb.Components.Toolbar.Cart
{
    [Widget(
        AutoInitialize = true,
        RefreshUrl = "/Widgets/Cart",
        StyleTypes = new[] { typeof(CartWidgetStyleContributor) },
        ScriptTypes = new[] { typeof(CartWidgetScriptContributor) }
    )]
    public class CartWidgetViewComponent : AbpViewComponent
    {
        private readonly UserBasketProvider _userBasketProvider;

        public CartWidgetViewComponent(UserBasketProvider userBasketProvider)
        {
            _userBasketProvider = userBasketProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(
                "~/Components/Toolbar/Cart/Default.cshtml",
                await _userBasketProvider.GetBasketAsync());
        }
    }
}
