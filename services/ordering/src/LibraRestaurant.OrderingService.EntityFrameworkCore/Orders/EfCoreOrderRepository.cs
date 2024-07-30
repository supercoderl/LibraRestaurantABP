using LibraRestaurant.OrderingService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Specifications;

namespace LibraRestaurant.OrderingService.Orders
{
    public class EfCoreOrderRepository : EfCoreRepository<OrderingServiceDbContext, Order, Guid>, IOrderRepository
    {
        public EfCoreOrderRepository(IDbContextProvider<OrderingServiceDbContext> dbContextProvider) : base(
            dbContextProvider)
        {
        }

        public async Task<List<Order>> GetOrdersByUserId(
            Guid userId,
            ISpecification<Order> spec,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            IQueryable<Order> query = includeDetails
                ? EfCoreOrderQueryableExtensions.IncludeDetails(dbSet)
                : dbSet;

            return await query
                .Where(q => q.Buyer.Id == userId)
                .Where(spec.ToExpression())
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Order>> GetOrdersAsync(
            ISpecification<Order> spec,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            IQueryable<Order> query = includeDetails
                ? EfCoreOrderQueryableExtensions.IncludeDetails(dbSet)
                : dbSet;

            return await query
                .Where(spec.ToExpression())
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Order>> GetDashboardAsync(
            ISpecification<Order> spec,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            IQueryable<Order> query = includeDetails
                ? EfCoreOrderQueryableExtensions.IncludeDetails(dbSet)
                : dbSet;

            return await query
                .Where(spec.ToExpression())
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<Order> GetByOrderNoAsync(
            int orderNo,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            var queryable = await GetQueryableAsync();
            IQueryable<Order> query = includeDetails
                ? EfCoreOrderQueryableExtensions.IncludeDetails(queryable)
                : queryable;

            return await query
                .FirstOrDefaultAsync(q => q.OrderNo == orderNo, GetCancellationToken(cancellationToken))
                ?? throw new EntityNotFoundException(typeof(Order)); //TODO: Maybe create new exception with property extending this
        }

        public override async Task<IQueryable<Order>> WithDetailsAsync()
        {
            return EfCoreOrderQueryableExtensions.IncludeDetails(await GetQueryableAsync());
            // or
            // return OrderEfCoreQueryableExtensions.IncludeDetails(await GetQueryableAsync());
        }
    }
}
