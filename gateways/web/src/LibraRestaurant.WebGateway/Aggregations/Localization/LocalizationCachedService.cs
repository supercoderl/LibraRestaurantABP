using LibraRestaurant.WebGateway.Aggregations.Base;
using Microsoft.Extensions.Caching.Memory;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
using Volo.Abp.DependencyInjection;

namespace LibraRestaurant.WebGateway.Aggregations.Localization
{
    public class LocalizationCachedService(IMemoryCache localizationCache)
        : CachedServiceBase<ApplicationLocalizationDto>(localizationCache), ISingletonDependency;
}
