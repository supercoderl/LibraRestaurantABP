using LibraRestaurant.WebGateway.Aggregations.Base;

namespace LibraRestaurant.WebGateway.Aggregations.ApplicationConfiguration
{
    public class AppConfigurationRequest : IRequestInput
    {
        public Dictionary<string, string> Endpoints { get; } = new();
    }
}
