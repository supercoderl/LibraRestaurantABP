using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;

namespace LibraRestaurant.WebGateway.Aggregations.Localization
{
    public interface ILocalizationAggregation
    {
        string LocalizationRouteName { get; }
        string LocalizationEndpoint { get; }
        Task<ApplicationLocalizationDto> GetLocalizationAsync(LocalizationRequest input);
    }
}
