using LibraRestaurant.OrderingService.Domain;
using LibraRestaurant.OrderingService.EntityFrameworkCore;
using LibraRestaurant.Shared.Hosting.Microservices.DbMigrations.EfCore;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace LibraRestaurant.OrderingService.DbMigrations
{
    public class OrderingServiceDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<OrderingServiceDbContext>
    {
        public OrderingServiceDatabaseMigrationChecker(
            IUnitOfWorkManager unitOfWorkManager,
            IServiceProvider serviceProvider,
            ICurrentTenant currentTenant,
            IDistributedEventBus distributedEventBus,
            IAbpDistributedLock abpDistributedLock)
            : base(
                unitOfWorkManager,
                serviceProvider,
                currentTenant,
                distributedEventBus,
                abpDistributedLock,
                OrderingServiceDbProperties.ConnectionStringName)
        {
        }
    }
}
