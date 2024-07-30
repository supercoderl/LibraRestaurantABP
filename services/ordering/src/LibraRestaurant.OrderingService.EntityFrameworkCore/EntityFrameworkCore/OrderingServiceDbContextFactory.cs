﻿using LibraRestaurant.OrderingService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.OrderingService.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class OrderingServiceDbContextFactory : IDesignTimeDbContextFactory<OrderingServiceDbContext>
    {
        public OrderingServiceDbContext CreateDbContext(string[] args)
        {
            OrderingServiceEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<OrderingServiceDbContext>()
                .UseNpgsql(
                    configuration.GetConnectionString(OrderingServiceDbProperties.ConnectionStringName),
                    b =>
                    {
                        b.MigrationsHistoryTable("__OrderingService_Migrations");
                    });

            // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return new OrderingServiceDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../LibraRestaurant.OrderingService.HttpApi.Host/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
