using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.CmskitService
{
    public static class CmskitServiceDbProperties
    {
        public static string DbTablePrefix { get; set; } = "";

        public static string? DbSchema { get; set; } = null;

        public const string ConnectionStringName = "CmskitService";
    }
}
