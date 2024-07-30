using LibraRestaurant.PaymentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace LibraRestaurant.PaymentService.Permissions
{
    public class PaymentServicePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(PaymentServicePermissions.GroupName, L("Permission:PaymentService"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<PaymentServiceResource>(name);
        }
    }
}
