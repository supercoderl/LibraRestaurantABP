﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace LibraRestaurant.OrderingService.Orders
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
        Task<List<Order>> GetOrdersByUserId(
            Guid userId,
            ISpecification<Order> spec,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Task<List<Order>> GetOrdersAsync(
            ISpecification<Order> spec,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Task<List<Order>> GetDashboardAsync(
            ISpecification<Order> spec,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Task<Order> GetByOrderNoAsync(int orderNo,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);
    }
}
