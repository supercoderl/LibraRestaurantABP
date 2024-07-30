using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace LibraRestaurant.PublicWeb.Pages
{
    public class MyOrdersModel : AbpPageModel
    {
        public string OrderFilter { get; set; }

        public Task OnGet(string filter)
        {
            OrderFilter = filter;
            return Task.CompletedTask;
        }
    }
}
