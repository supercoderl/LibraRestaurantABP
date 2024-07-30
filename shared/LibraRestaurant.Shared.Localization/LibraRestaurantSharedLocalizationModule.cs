using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using LibraRestaurant.Localization;

namespace LibraRestaurant
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class LibraRestaurantSharedLocalizationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<LibraRestaurantSharedLocalizationModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<LibraRestaurantResource>("en")
                    .AddBaseTypes(
                        typeof(AbpValidationResource)
                    ).AddVirtualJson("/Localization/LibraRestaurant");

                options.DefaultResourceType = typeof(LibraRestaurantResource);
            });
        }
    }
}
