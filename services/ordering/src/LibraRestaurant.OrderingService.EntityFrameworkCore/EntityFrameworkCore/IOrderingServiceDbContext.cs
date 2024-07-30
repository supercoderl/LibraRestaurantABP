using LibraRestaurant.OrderingService.Domain;
using LibraRestaurant.OrderingService.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace LibraRestaurant.OrderingService.EntityFrameworkCore
{
    [ConnectionStringName(OrderingServiceDbProperties.ConnectionStringName)]
    public interface IOrderingServiceDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<Order> Orders { get; }
    }
}
