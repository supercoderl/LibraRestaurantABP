﻿using Volo.Abp.Caching;

namespace LibraRestaurant.BasketService.Entities
{
    public class BasketRepository : IBasketRepository
    {
        public bool? IsChangeTrackingEnabled { get; set; }

        private readonly IDistributedCache<Basket, Guid> _cache;

        public BasketRepository(IDistributedCache<Basket, Guid> cache)
        {
            _cache = cache;
        }

        public async Task<Basket> GetAsync(Guid id)
        {
            return (await _cache.GetOrAddAsync(id, () => Task.FromResult(new Basket(id))))!;
        }

        public async Task UpdateAsync(Basket basket)
        {
            await _cache.SetAsync(basket.Id, basket);
        }
    }
}
