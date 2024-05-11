using RabbitMQ.Client;
using System.Text;

namespace UdemyRabbitMQ.publisher
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //MESAJI DİREKT OLARAK KUYRUGA GÖNDERME YÖNTEMİ 

            //var factory = new ConnectionFactory();

            //factory.Uri = new Uri("amqps://cezgimih:Xo_fII9ov60oS1bqMUETJEKjNyAK-JC3@toad.rmq.cloudamqp.com/cezgimih");


            //using var connection = factory.CreateConnection();

            //var channel = connection.CreateModel();


            //channel.QueueDeclare("hello-queue", true, false, false); //ikinci parametre false veya true olması verilerin memoryde yada fiziksel olarak tutulması true yaparsak fiziksel tutulur ve rabbitmq'a restart atsak dahi veriler silinmez. //üçüncü parametre ise buradaki kuyruğa farklı bir kanal üstünden baglanmaya sağlar.dördüncü parametre ise subcriber down olursa kuyruk silinmemsini sağlar kuyruk işleme devam etsin.


            //Enumerable.Range(1, 50).ToList().ForEach(x =>
            //{
            //    string message = $"Message {x}";

            //    var messageBody = Encoding.UTF8.GetBytes(message);

            //    channel.BasicPublish(string.Empty, "hello-queue", null, messageBody); //Mesajı gönderiyoruz. Birinci parametre exchange değil direkt kuyruğa göndereceksek empty yapıyoruz. 2. parametre göndereceğimiz kuyrul. 3. parametre ekstra bir özellik olacakmı. 4. parametre gönderilecek veri.

            //    Console.WriteLine($"Mesaj gönderilmiştir. : {message}");


            //});


            //Console.ReadLine();






            //KUYRUGU PUBLİSHER OLARAK OLUSTURMAMA. FANOUT EXCHANGE

            var factory = new ConnectionFactory();

            factory.Uri = new Uri("amqps://cezgimih:Xo_fII9ov60oS1bqMUETJEKjNyAK-JC3@toad.rmq.cloudamqp.com/cezgimih");


            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            Enumerable.Range(1, 50).ToList().ForEach(x =>
            {
                string message = $"Message {x}";

                var messageBody = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(string.Empty, "hello-queue", null, messageBody); //Mesajı gönderiyoruz. Birinci parametre exchange değil direkt kuyruğa göndereceksek empty yapıyoruz. 2. parametre göndereceğimiz kuyrul. 3. parametre ekstra bir özellik olacakmı. 4. parametre gönderilecek veri.

                Console.WriteLine($"Mesaj gönderilmiştir. : {message}");


            });


            Console.ReadLine();

        }
    }
}
