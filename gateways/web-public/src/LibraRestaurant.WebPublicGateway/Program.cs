using LibraRestaurant.Shared.Hosting.AspNetCore;
using Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LibraRestaurant.Shared.Hosting.Gateways;

namespace LibraRestaurant.WebPublicGateway
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

                var builder = WebApplication.CreateBuilder(args);
                builder.Host
                    .AddYarpJson()
                    .UseAutofac()
                    .UseSerilog();

                builder.AddServiceDefaults();

                await builder.AddApplicationAsync<LibraRestaurantWebPublicGatewayModule>();
                var app = builder.Build();
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
                await Log.CloseAndFlushAsync();
            }
        }
    }
}
