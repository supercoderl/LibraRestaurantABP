using LibraRestaurant.WebGateway.Aggregations.Base;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;

namespace LibraRestaurant.WebGateway.Aggregations.ApplicationConfiguration
{
    public interface IAppConfigurationRemoteService : IAggregateRemoteService<ApplicationConfigurationDto>;
}
