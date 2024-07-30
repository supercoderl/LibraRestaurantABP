using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Reflection;

namespace LibraRestaurant.CatalogService.Permissions
{
    public static class CatalogServicePermissions
    {
        public const string GroupName = "CatalogService";

        public static class Products
        {
            public const string Default = GroupName + ".Products";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(CatalogServicePermissions));
        }
    }
}
