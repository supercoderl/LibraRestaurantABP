using LibraRestaurant.CatalogService.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace LibraRestaurant.CatalogService.MongoDB
{
    public static class CatalogServiceMongoDbContextExtensions
    {
        public static void ConfigureCatalogService(
            this IMongoModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Product>(x =>
            {
                x.CollectionName = "Products";
            });
        }
    }
}
