using LibraRestaurant.CatalogService.Domain;
using LibraRestaurant.CatalogService.MongoDB;
using LibraRestaurant.Shared.Hosting.Microservices.DbMigrations;
using System.Data;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace LibraRestaurant.CatalogService.DbMigrations
{
    public class CatalogServiceDatabaseMigrationChecker : PendingMongoDbMigrationsChecker<CatalogServiceMongoDbContext>
    {
        public CatalogServiceDatabaseMigrationChecker(
            IUnitOfWorkManager unitOfWorkManager,
            IServiceProvider serviceProvider,
            ICurrentTenant currentTenant,
            IDataSeeder dataSeeder,
            IAbpDistributedLock distributedLockProvider)
            : base(
                unitOfWorkManager,
                serviceProvider,
                currentTenant,
                dataSeeder,
                distributedLockProvider,
                CatalogServiceDbProperties.ConnectionStringName)
        {
        }
    }
}
