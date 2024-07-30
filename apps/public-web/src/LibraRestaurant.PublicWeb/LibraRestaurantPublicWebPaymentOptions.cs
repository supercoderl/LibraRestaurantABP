using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.PublicWeb
{
    public class LibraRestaurantPublicWebPaymentOptions
    {
        public string PaymentSuccessfulCallbackUrl { get; set; }
        public string PaymentFailureCallbackUrl { get; set; }
    }
}
