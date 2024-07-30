using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.PaymentService
{
    public static class PaymentServiceDbProperties
    {
        public static string DbTablePrefix { get; set; } = "PaymentService";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "PaymentService";
    }
}
