using LibraRestaurant.Shared.Hosting.AspNetCore;
using LibraRestaurant.Shared.Hosting.Gateways;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace LibraRestaurant.WebGateway
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
                    .AddAppSettingsSecretsJson()
                    .AddYarpJson()
                    .UseAutofac()
                    .UseSerilog();

                builder.AddServiceDefaults();

                await builder.AddApplicationAsync<LibraRestaurantWebGatewayModule>();
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
                Log.CloseAndFlush();
            }
        }
    }
}
