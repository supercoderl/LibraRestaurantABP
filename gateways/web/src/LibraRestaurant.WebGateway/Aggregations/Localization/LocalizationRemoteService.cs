using LibraRestaurant.WebGateway.Aggregations.Base;
using System.Text.Json;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace LibraRestaurant.WebGateway.Aggregations.Localization
{
    public class LocalizationRemoteService : AggregateRemoteServiceBase<ApplicationLocalizationDto>,
        ILocalizationRemoteService, ITransientDependency
    {
        public LocalizationRemoteService(
            IHttpContextAccessor httpContextAccessor,
            IJsonSerializer jsonSerializer,
            ILogger<AggregateRemoteServiceBase<ApplicationLocalizationDto>> logger)
            : base(httpContextAccessor, jsonSerializer, logger)
        {
        }
    }
}
