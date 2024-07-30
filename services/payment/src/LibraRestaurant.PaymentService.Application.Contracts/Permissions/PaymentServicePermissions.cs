using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Reflection;

namespace LibraRestaurant.PaymentService.Permissions
{
    public class PaymentServicePermissions
    {
        public const string GroupName = "PaymentService";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(PaymentServicePermissions));
        }
    }
}
