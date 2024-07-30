using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.AdministrationService.Domain
{
    public static class AdministrationServiceDbProperties
    {
        public static string DbTablePrefix { get; set; } = string.Empty;

        public static string? DbSchema { get; set; } = null;

        public const string ConnectionStringName = "AdministrationService";
    }
}
