using LibraRestaurant.CmskitService.Localization;
using LibraRestaurant.CmskitService.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace LibraRestaurant.CmskitService.Permissions
{
    public class CmskitServicePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(CmskitServicePermissions.GroupName, L("Permission:CmskitService"));

            //Define your own permissions here. Example:
            //myGroup.AddPermission(BookStorePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CmskitServiceResource>(name);
        }
    }
}
