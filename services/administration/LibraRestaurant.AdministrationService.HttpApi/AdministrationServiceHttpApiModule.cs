using LibraRestaurant.AdministrationService.Application.Contracts;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;

namespace LibraRestaurant.AdministrationService.HttpApi
{
    [DependsOn(
        typeof(AdministrationServiceApplicationContractsModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpSettingManagementHttpApiModule)
        )]
    public class AdministrationServiceHttpApiModule : AbpModule
    {

    }
}
