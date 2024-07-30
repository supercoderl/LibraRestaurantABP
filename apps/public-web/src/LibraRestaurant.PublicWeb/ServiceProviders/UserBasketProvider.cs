﻿using LibraRestaurant.BasketService.Services;
using LibraRestaurant.Shared.Hosting.AspNetCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LibraRestaurant.PublicWeb.ServiceProviders
{
    public class UserBasketProvider : ITransientDependency
    {
        private HttpContext? HttpContext => _httpContextAccessor.HttpContext;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBasketAppService _basketAppService;

        public UserBasketProvider(
            IHttpContextAccessor httpContextAccessor,
            IBasketAppService basketAppService)
        {
            _httpContextAccessor = httpContextAccessor;
            _basketAppService = basketAppService;
        }

        public virtual async Task<BasketDto> GetBasketAsync()
        {
            var anonymousUserId = await GetAnonymousUserId();

            return await _basketAppService.GetAsync(Guid.Parse(anonymousUserId));
        }

        // Get anonymous user id from cookie
        private async Task<string> GetAnonymousUserId()
        {
            if (HttpContext?.Request.Cookies.TryGetValue(LibraRestaurantConstants.AnonymousUserClaimName, out string? anonymousUserId) != true || string.IsNullOrEmpty(anonymousUserId))
            {
                // Generate guid for anonymous user id and set to cookie for 14 days
                anonymousUserId = Guid.NewGuid().ToString();
                HttpContext?.Response.Cookies.Append(LibraRestaurantConstants.AnonymousUserClaimName, anonymousUserId,
                    new CookieOptions
                    {
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTimeOffset.UtcNow.AddDays(14)
                    });
            }

            return await Task.FromResult(anonymousUserId);
        }
    }
}
