using RabbitMQ.Client;

namespace UdemyRabbitMQWeb.Watermark.Services
{
    public class RabbitMQClientService : IDisposable
    {

        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string ExchangeName = "ImageDirectExchange";
        public static string RoutingWatermark = "watermark-route-image";
        public static string QueueName = "queue-watermark-image";

        private readonly ILogger<RabbitMQClientService> _logger;

        public RabbitMQClientService(ConnectionFactory connectionFactory,ILogger<RabbitMQClientService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }


        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();
            //if(_channel.IsOpen == true)
            if(_channel is { IsOpen: true })
            {
                return _channel;
            }

            _channel= _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName,type:"direct",true,false);

            _channel.QueueDeclare(QueueName,durable:true,false,false,null);

            _channel.QueueBind(exchange:ExchangeName,queue:QueueName,routingKey:RoutingWatermark);

            _logger.LogInformation("RabbitMq ile bağlantı kuruldu");

            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();  
            _channel?.Dispose();
            //_channel = default; // null'dan farkı default değeri neyse onu verir örnek string "", bool ise false vb.
            _connection?.Close();
            _connection?.Dispose();

            _logger.LogInformation("RabbitMQ ile bağlantı koptu");

        }
    }
}
