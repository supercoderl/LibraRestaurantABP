using LibraRestaurant.PublicWeb.Components.Basket;
using LibraRestaurant.PublicWeb.Components.Toolbar.Cart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace LibraRestaurant.PublicWeb.Controllers
{
    [Route("Widgets")]
    public class WidgetsController : AbpController
    {
        [HttpGet]
        [Route("Basket")]
        public IActionResult Counters()
        {
            return ViewComponent(typeof(BasketWidgetViewComponent));
        }

        [HttpGet]
        [Route("Cart")]
        public IActionResult GetCartWidget()
        {
            return ViewComponent(typeof(CartWidgetViewComponent));
        }
    }
}
