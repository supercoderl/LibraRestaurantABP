﻿using LibraRestaurant.OrderingService.Orders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;
using Volo.Abp.AspNetCore.Mvc;

namespace LibraRestaurant.PublicWeb.Components.UserOrders
{
    [Widget(
        AutoInitialize = true,
        RefreshUrl = "/Widgets/MyOrders",
        StyleTypes = new[] { typeof(UserOrderWidgetStyleContributor) },
        ScriptTypes = new[] { typeof(UserOrderWidgetScriptContributor) }
    )]
    public class UserOrderWidgetViewComponent : AbpViewComponent
    {
        private readonly IOrderAppService _orderAppService;
        public UserOrderViewModel ViewModel { get; set; } = new();

        public UserOrderWidgetViewComponent(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string filter)
        {
            try
            {
                ViewModel.UserOrders = await _orderAppService.GetMyOrdersAsync(new GetMyOrdersInput { Filter = filter });
            }
            catch (Exception e)
            {
                ViewModel.ServiceError = true;
                ViewModel.UserOrders = new List<OrderDto>();
                Console.WriteLine(e);
            }

            return View("~/Components/UserOrders/Default.cshtml", ViewModel);
        }

        public class UserOrderViewModel
        {
            public List<OrderDto> UserOrders { get; set; }
            public bool ServiceError { get; set; }
        }
    }
}
