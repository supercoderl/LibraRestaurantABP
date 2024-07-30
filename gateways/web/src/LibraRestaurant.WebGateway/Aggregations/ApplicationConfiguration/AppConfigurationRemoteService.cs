using LibraRestaurant.WebGateway.Aggregations.Base;
using System.Text.Json;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace LibraRestaurant.WebGateway.Aggregations.ApplicationConfiguration
{
    public class AppConfigurationRemoteService(
        IHttpContextAccessor httpContextAccessor,
        IJsonSerializer jsonSerializer,
        ILogger<AggregateRemoteServiceBase<ApplicationConfigurationDto>> logger)
        : AggregateRemoteServiceBase<ApplicationConfigurationDto>(httpContextAccessor, jsonSerializer, logger),
            IAppConfigurationRemoteService, ITransientDependency;
}
