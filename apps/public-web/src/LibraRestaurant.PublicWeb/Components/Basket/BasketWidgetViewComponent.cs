using LibraRestaurant.PublicWeb.ServiceProviders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;
using Volo.Abp.AspNetCore.Mvc;

namespace LibraRestaurant.PublicWeb.Components.Basket
{
    [Widget(
        AutoInitialize = true,
        RefreshUrl = "/Widgets/Basket",
        StyleFiles = new[] { "/components/basket/basket-widget.css" },
        ScriptTypes = new[] { typeof(BasketWidgetScriptContributor) }
    )]
    public class BasketWidgetViewComponent : AbpViewComponent
    {
        private readonly UserBasketProvider _userBasketProvider;

        public BasketWidgetViewComponent(UserBasketProvider userBasketProvider)
        {
            _userBasketProvider = userBasketProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(
                "~/Components/Basket/Default.cshtml",
                await _userBasketProvider.GetBasketAsync());
        }
    }
}
