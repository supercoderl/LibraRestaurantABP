using LibraRestaurant.BasketService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;
using Volo.Abp.AspNetCore.Mvc;

namespace LibraRestaurant.PublicWeb.Components.Purchase
{
    [Widget(
        AutoInitialize = true,
        RefreshUrl = "/Widgets/Basket",
        StyleTypes = new[] { typeof(PurchaseWidgetStyleContributor) },
        ScriptTypes = new[] { typeof(PurchaseWidgetScriptContributor) }
    )]
    public class PurchaseWidgetViewComponent : AbpViewComponent
    {
        public PurchaseViewModel ViewModel { get; set; }

        public Task<IViewComponentResult> InvokeAsync(string buttonDescription, BasketDto basket)
        {
            ViewModel = new PurchaseViewModel
            {
                ButtonDescription = buttonDescription,
                Basket = basket
            };

            return Task.FromResult<IViewComponentResult>(
                View("~/Components/Purchase/Default.cshtml", ViewModel)
            );
        }

        public class PurchaseViewModel
        {
            public string ButtonDescription { get; set; }
            public BasketDto Basket { get; set; }
        }
    }
}
