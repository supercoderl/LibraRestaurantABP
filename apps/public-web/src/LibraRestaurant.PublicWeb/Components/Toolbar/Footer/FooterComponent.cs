using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace LibraRestaurant.PublicWeb.Components.Toolbar.Footer
{
    public class FooterComponent : AbpViewComponent
    {
        private readonly FooterModel _model = new();

        public FooterComponent()
        {
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(AbpViewComponent).Assembly.Location);
            _model.AbpVersion = fvi.FileVersion;
        }

        public virtual IViewComponentResult Invoke()
        {
            return View("~/Components/Toolbar/Footer/Default.cshtml", _model);
        }
    }

    public class FooterModel
    {
        public string AbpVersion { get; set; }
    }
}
