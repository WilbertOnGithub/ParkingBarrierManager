using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Azure.Messaging.ServiceBus;

namespace Arentheym.ParkingBarrier.SMSResponseReceiver
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = FunctionsApplication.CreateBuilder(args);

            builder.ConfigureFunctionsWebApplication();

            builder.Services
                .AddApplicationInsightsTelemetryWorkerService()
                .ConfigureFunctionsApplicationInsights();

            builder.Services.AddSingleton(_ =>
            {
                var connectionString = Environment.GetEnvironmentVariable("ServiceBusConnectionString");
                return new ServiceBusClient(connectionString);
            });

            builder.Build().Run();
        }
    }
}
