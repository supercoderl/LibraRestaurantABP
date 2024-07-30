using LibraRestaurant.CmskitService.EntityFrameworkCore;
using LibraRestaurant.Shared.Hosting.Microservices.DbMigrations.EfCore;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace LibraRestaurant.CmskitService.DbMigrations
{
    public class CmskitServiceDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<CmskitServiceDbContext>
    {
        public CmskitServiceDatabaseMigrationChecker(
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
                CmskitServiceDbProperties.ConnectionStringName)
        {
        }
    }
}
