using LibraRestaurant.AdministrationService.Domain;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.AdministrationService.EntityFrameworkCore
{
    public class AdministrationServiceDbContextFactory : IDesignTimeDbContextFactory<AdministrationServiceDbContext>
    {
        public AdministrationServiceDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AdministrationServiceDbContext>()
                .UseNpgsql(GetConnectionStringFromConfiguration(), b =>
                {
                    b.MigrationsHistoryTable("__AdministrationService_Migrations");
                });

            // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return new AdministrationServiceDbContext(builder.Options);
        }

        private static string? GetConnectionStringFromConfiguration()
        {
            return BuildConfiguration()
                .GetConnectionString(AdministrationServiceDbProperties.ConnectionStringName);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        $"..{Path.DirectorySeparatorChar}LibraRestaurant.AdministrationService.HttpApi.Host"
                    )
                )
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
