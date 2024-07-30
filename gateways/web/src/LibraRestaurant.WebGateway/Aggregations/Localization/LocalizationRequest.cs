﻿using LibraRestaurant.WebGateway.Aggregations.Base;

namespace LibraRestaurant.WebGateway.Aggregations.Localization
{
    public class LocalizationRequest : IRequestInput
    {
        public Dictionary<string, string> Endpoints { get; } = new();
        public string CultureName { get; set; }

        public LocalizationRequest(string cultureName)
        {
            CultureName = cultureName;
        }
    }
}
