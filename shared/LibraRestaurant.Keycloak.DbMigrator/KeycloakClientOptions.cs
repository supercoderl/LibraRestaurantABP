using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.DbMigrator
{
    public class KeycloakClientOptions
    {
        public string Url { get; set; }
        public string AdminUserName { get; set; }
        public string AdminPassword { get; set; }
        public string RealmName { get; set; }
    }
}
