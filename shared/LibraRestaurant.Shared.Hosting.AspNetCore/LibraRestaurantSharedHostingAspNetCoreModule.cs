using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;

namespace LibraRestaurant.Shared.Hosting.AspNetCore
{
    [DependsOn(
        typeof(LibraRestaurantSharedHostingModule),
        typeof(LibraRestaurantSharedLocalizationModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule)
    )]
    public class LibraRestaurantSharedHostingAspNetCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<LibraRestaurantSharedHostingAspNetCoreModule>("LibraRestaurant.Shared.Hosting.AspNetCore");
            });
        }
    }
}
