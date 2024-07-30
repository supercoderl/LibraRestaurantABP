using LibraRestaurant.PaymentService.EntityFrameworkCore;
using LibraRestaurant.Shared.Hosting.Microservices.DbMigrations.EfCore;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace LibraRestaurant.PaymentService.DbMigrations
{
    public class PaymentServiceDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<PaymentServiceDbContext>
    {
        public PaymentServiceDatabaseMigrationChecker(
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
                PaymentServiceDbProperties.ConnectionStringName)
        {
        }
    }
}
