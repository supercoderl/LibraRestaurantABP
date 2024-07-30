using LibraRestaurant.CatalogService.Domain;
using LibraRestaurant.CatalogService.Products;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace LibraRestaurant.CatalogService.MongoDB
{
    [ConnectionStringName(CatalogServiceDbProperties.ConnectionStringName)]
    public class CatalogServiceMongoDbContext : AbpMongoDbContext, ICatalogServiceMongoDbContext
    {
        public IMongoCollection<Product> Products => Collection<Product>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureCatalogService();
        }
    }
}
