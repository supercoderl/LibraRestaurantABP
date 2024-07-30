using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.OrderingService.Orders
{
    public static class EfCoreOrderQueryableExtensions
    {
        public static IQueryable<Order> IncludeDetails(this IQueryable<Order> queryable, bool include = true)
        {
            return !include
                ? queryable
                : queryable
                    .Include(q => q.OrderStatus)
                    .Include(q => q.Address)
                    .Include(q => q.Buyer)
                    .Include(q => q.OrderItems);
        }
    }
}
