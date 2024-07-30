using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.PaymentService.PaymentRequests
{
    public static class PaymentRequestConsts
    {
        public const int MaxCurrencyLength = 3;
        public const int MinOrderIdLength = 36;
        public const int MaxOrderIdLength = 36;
        public const int MaxCodeLength = 32;
        public const int MaxNameLength = 256;
    }
}
