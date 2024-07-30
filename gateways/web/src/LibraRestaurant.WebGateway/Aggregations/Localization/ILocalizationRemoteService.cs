using LibraRestaurant.WebGateway.Aggregations.Base;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;

namespace LibraRestaurant.WebGateway.Aggregations.Localization
{
    public interface ILocalizationRemoteService : IAggregateRemoteService<ApplicationLocalizationDto>;
}
