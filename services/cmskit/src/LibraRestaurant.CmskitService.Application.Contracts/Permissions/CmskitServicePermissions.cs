using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Reflection;

namespace LibraRestaurant.CmskitService.Permissions
{
    public class CmskitServicePermissions
    {
        public const string GroupName = "CmskitService";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(CmskitServicePermissions));
        }
    }
}
