using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.OrderingService.Domain
{
    public static class OrderingServiceDbProperties
    {
        public static string DbTablePrefix { get; set; } = "";

        public static string DbSchema { get; set; }

        public const string ConnectionStringName = "OrderingService";
    }
}
