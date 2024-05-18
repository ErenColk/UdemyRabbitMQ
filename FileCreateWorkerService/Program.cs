using FileCreateWorkerService.Services;
using RabbitMQ.Client;

namespace FileCreateWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHostedService<Worker>();
                   services.AddSingleton<RabbitMQClientService>();

                   // Configuration'� do�rudan hostContext �zerinden al�yoruz
                   IConfiguration configuration = hostContext.Configuration;

                   services.AddSingleton(sp => new ConnectionFactory()
                   {
                       Uri = new Uri(configuration.GetConnectionString("RabbitMQ")),
                       DispatchConsumersAsync = true
                   });
               })
                .Build();

            host.Run();
        }
    }
}