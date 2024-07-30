using Serilog;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using LibraRestaurant.Shared.Hosting.AspNetCore;

namespace LibraRestaurant.IdentityService
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var assemblyName = typeof(Program).Assembly.GetName().Name;

            SerilogConfigurationHelper.Configure(assemblyName);

            try
            {
                Log.Information($"Starting {assemblyName}.");
                var app = await ApplicationBuilderHelper
                    .BuildApplicationAsync<IdentityServiceHttpApiHostModule>(args);
                await app.InitializeApplicationAsync();
                await app.RunAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"{assemblyName} terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
