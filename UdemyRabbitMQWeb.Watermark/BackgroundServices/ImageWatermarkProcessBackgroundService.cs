using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Drawing;
using System.Text;
using System.Text.Json;
using UdemyRabbitMQWeb.Watermark.Services;

namespace UdemyRabbitMQWeb.Watermark.BackgroundServices
{
    public class ImageWatermarkProcessBackgroundService : BackgroundService
    {
        private readonly RabbitMQClientService _rabbitmqClientService;
        private readonly ILogger<ImageWatermarkProcessBackgroundService> _logger;
        private IModel _channel;
        public ImageWatermarkProcessBackgroundService(RabbitMQClientService rabbitmqClientService, ILogger<ImageWatermarkProcessBackgroundService> logger)
        {
            _rabbitmqClientService = rabbitmqClientService;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {

            _channel = _rabbitmqClientService.Connect();
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            _channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);


            consumer.Received += Consumer_Received;

            return Task.CompletedTask;

        }

        private Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            try
            {

                var productImageCreatedEvent = JsonSerializer.Deserialize<ProductImageCreatedEvent>(Encoding.UTF8.GetString(@event.Body.ToArray())); // Gelen veriyi deserialize ediyoruz.

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", productImageCreatedEvent.ImageName);

                var siteName = "www.mysite.com";

                using var img = Image.FromFile(path);

                using var graphic = Graphics.FromImage(img);

                var font = new Font(FontFamily.GenericMonospace, 40, FontStyle.Bold, GraphicsUnit.Pixel); //Fontunu ayarladık

                var textSize = graphic.MeasureString(siteName, font); //Textin uzunlugunu belişrliytoruz.

                var color = Color.FromArgb(128, 255, 255, 255);
                var brush = new SolidBrush(color); // Yazı yazma işlemi

                var position = new Point(img.Width - ((int)textSize.Width + 30), img.Height - ((int)textSize.Height + 30));

                graphic.DrawString(siteName, font, brush, position);

                img.Save("wwwroot/Images/watermarks/" + productImageCreatedEvent.ImageName);

                img.Dispose(); // Bellekte yer kaplamaması için dispose edilir
                graphic.Dispose();

                _channel.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
            }

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
