using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.PaymentService.PaymentMethods
{
    public static class PaymentMethodNames
    {
        /*
         * PaymentTypes must be unique.
         * It's identifier of a PaymentMethod.
         */
        public const string Demo = "demo";

        public const string PayPal = "paypal";
    }
}
