using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.PublicWeb
{
    public class LibraRestaurantPaymentConsts
    {
        public const string Currency = "USD";
        public const string PaymentMethodCookie = "selected_payment_method"; // Setted in payment-widget.js

        public static class DemoAddressTypes
        {
            public const string Work = "Work";
            public const string Home = "Home";
        }
    }
}
