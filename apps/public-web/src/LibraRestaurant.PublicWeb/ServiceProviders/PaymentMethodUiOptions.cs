using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.PublicWeb.ServiceProviders
{
    public class PaymentMethodUiOptions
    {
        [NotNull]
        public Dictionary<string, string> Icons { get; } = new();

        public string DefaultIcon { get; set; } = "fa-credit-card demo";

        public void ConfigureIcon(string paymentMethod, string iconCss)
        {
            Icons[paymentMethod] = iconCss;
        }
    }
}
