using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace LibraRestaurant.CmskitService.EntityFrameworkCore
{
    public static class CmskitServiceDbContextModelCreatingExtensions
    {
        public static void ConfigureCmskitService(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(CmskitServiceConsts.DbTablePrefix + "YourEntities", CmskitServiceConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}
