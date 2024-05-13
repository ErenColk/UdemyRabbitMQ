using RabbitMQ.Client;
using Shared;
using System.Text;
using System.Text.Json;

namespace UdemyRabbitMQ.publisher
{
    internal class Program
    {

        public enum LogNames
        {
            Ciritical = 1,
            Error = 2,
            Warning = 3,
            Info = 4,

        }



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




            //FANOUT EXCHANGE
            ////KUYRUGU PUBLİSHER OLARAK OLUSTURMAMA. 

            //var factory = new ConnectionFactory();

            //factory.Uri = new Uri("amqps://cezgimih:Xo_fII9ov60oS1bqMUETJEKjNyAK-JC3@toad.rmq.cloudamqp.com/cezgimih");

            //using var connection = factory.CreateConnection();

            //var channel = connection.CreateModel();

            //channel.ExchangeDeclare("logs-fanout",durable:true,type:ExchangeType.Fanout);  // Exchange olusturuyoruz. 1.paramatre exchange adı. 2.parametre uygulama restartta atsa exchange kaybolmasın. Ayrıca exchange veya queueleri memoryde değil fiziksel yerde kaydeder. RabbitMq uygulaması restart edilse dahi mesajlar silinmez.

            //Enumerable.Range(1, 50).ToList().ForEach(x =>
            //{
            //    string message = $"log {x}";

            //    var messageBody = Encoding.UTF8.GetBytes(message);
            //    channel.BasicPublish("logs-fanout","", null, messageBody); //1.parametrede exchange'in adını veriyoruz. 2.parametrede kuyrugu publisher olusturmayacağı için boş bırakıyoruz.

            //    Console.WriteLine($"Mesaj gönderilmiştir. : {message}");
            //});



            //Console.ReadLine();






            //DIRECT EXCHANGE
            //KUYRUGU PUBLİSHER OLARAK OLUSTURMAMA.

            //var factory = new ConnectionFactory(); // RabbitMQ bağlantı fabrikası oluşturuluyor.

            //factory.Uri = new Uri("amqps://cezgimih:Xo_fII9ov60oS1bqMUETJEKjNyAK-JC3@toad.rmq.cloudamqp.com/cezgimih"); // RabbitMQ bağlantı adresi belirleniyor.

            //using var connection = factory.CreateConnection(); // Bağlantı oluşturuluyor ve "using" bloğu içinde otomatik olarak temizleniyor.

            //var channel = connection.CreateModel(); // Kanal (channel) oluşturuluyor.

            //channel.ExchangeDeclare("logs-direct", durable: true, type: ExchangeType.Direct); // Direct tipinde bir exchange oluşturuluyor.


            //Enum.GetNames(typeof(LogNames)).ToList().ForEach(x =>
            //{
            //    // Log türlerine göre kuyruk (queue) ve bağlantılar (bindings) tanımlanıyor.
            //    var routeKey = $"route-{x}"; 
            //    var queueName = $"direct-queue-{x}";
            //    channel.QueueDeclare(queueName, true, false, false);
            //    channel.QueueBind(queueName, "logs-direct", routeKey, null);
            //});

            //// Rastgele log mesajları yayınlamak için bir döngü oluşturuluyor.
            //Enumerable.Range(1, 50).ToList().ForEach(x =>
            //{
            //    LogNames log = (LogNames)new Random().Next(1, 5); // Rastgele bir log türü seçiliyor.

            //    string message = $"log-type : {log}"; // Log mesajı oluşturuluyor.

            //    var messageBody = Encoding.UTF8.GetBytes(message); // Mesaj byte dizisine dönüştürülüyor.

            //    var routeKey = $"route-{log}"; // Log türüne göre routeKey belirleniyor.

            //    // Log mesajı yayınlanıyor.
            //    channel.BasicPublish("logs-direct", routeKey, null, messageBody);

            //    Console.WriteLine($"Log gönderilmiştir. : {message}"); // Yayınlanan log mesajı konsola yazdırılıyor.
            //});


            //Console.ReadLine();

            //TOPIC EXCHANGE
            //Route keyler Critical.Error.Warning gibi belirlenir. Yani noktalarla belirlenir.Consumer olarak mesajı dinlemek istiyorum direkt olarak route keyi yazıyoruz.Kuyruk oluşturma işlemi consumera bırakılır. .*Error.* : Ortasında Error yazsın sağında ve solunda ne yazdıgı önemli değil. #.Error : son kısmı error olanlar gelsin demektir. Detaylı routelama yapmak istediğimizde kullnacağımız Exchange tipidir.



            //var factory = new ConnectionFactory(); // RabbitMQ bağlantı fabrikası oluşturuluyor.

            //factory.Uri = new Uri("amqps://cezgimih:Xo_fII9ov60oS1bqMUETJEKjNyAK-JC3@toad.rmq.cloudamqp.com/cezgimih"); // RabbitMQ bağlantı adresi belirleniyor.

            //using var connection = factory.CreateConnection(); // Bağlantı oluşturuluyor ve "using" bloğu içinde otomatik olarak temizleniyor.

            //var channel = connection.CreateModel(); // Kanal (channel) oluşturuluyor.

            //channel.ExchangeDeclare("logs-topic", durable: true, type: ExchangeType.Topic); // Topic tipinde bir exchange oluşturuluyor.


            //// Rastgele log mesajları yayınlamak için bir döngü oluşturuluyor.
            //Random rnd = new Random();
            //Enumerable.Range(1, 50).ToList().ForEach(x =>
            //{
            //    LogNames log = (LogNames)new Random().Next(1, 5); // Rastgele bir log türü seçiliyor.

            //    LogNames log1 = (LogNames)rnd.Next(1, 5);
            //    LogNames log2 = (LogNames)rnd.Next(1, 5);
            //    LogNames log3 = (LogNames)rnd.Next(1, 5);

            //    var routeKey = $"{log1}.{log2}.{log3}";


            //    string message = $"log-type : {log1}-{log2}-{log3}"; // Log mesajı oluşturuluyor.

            //    var messageBody = Encoding.UTF8.GetBytes(message); // Mesaj byte dizisine dönüştürülüyor.

            //    // Log mesajı yayınlanıyor.
            //    channel.BasicPublish("logs-topic", routeKey, null, messageBody);

            //    Console.WriteLine($"Log gönderilmiştir. : {message}"); // Yayınlanan log mesajı konsola yazdırılıyor.
            //});


            //Console.ReadLine();





            ////HEADER EXCHANGE
            ////Routelama ile ilgili bilgileri Headerında gönderiyoruz. Key value lerin aynı olmasına bakıyoruz. format = pdf , shape = a4 gibi. x-match = any : iki değerden birinin uyması yeterli demek x-match = all dersek ikisininde uyması gerektiğini belirtiyoruz.

            //var factory = new ConnectionFactory(); // RabbitMQ bağlantı fabrikası oluşturuluyor.

            //factory.Uri = new Uri("amqps://cezgimih:Xo_fII9ov60oS1bqMUETJEKjNyAK-JC3@toad.rmq.cloudamqp.com/cezgimih"); // RabbitMQ bağlantı adresi belirleniyor.

            //using var connection = factory.CreateConnection(); // Bağlantı oluşturuluyor ve "using" bloğu içinde otomatik olarak temizleniyor.

            //var channel = connection.CreateModel(); // Kanal (channel) oluşturuluyor.

            //channel.ExchangeDeclare("header-exchange", durable: true, type: ExchangeType.Headers); // Topic tipinde bir exchange oluşturuluyor.

            //Dictionary<string, object> headers = new Dictionary<string, object>();

            //headers.Add("format", "pdf");
            //headers.Add("share", "a4");

            //var properties = channel.CreateBasicProperties(); // Propertyler olusturulur ve headerları belirlenir.
            //properties.Headers = headers;

            ///*NOT*/
            //properties.Persistent = true; //Gönderilen mesajlar kalıcı hale gelir. Bütün Exchangetypelar için geçerlidir. Aynı şekilde properties olusturulur ve persistentları true yapılır. Header eklememize gerek kalmadan.

            //channel.BasicPublish("header-exchange", string.Empty, properties, Encoding.UTF8.GetBytes("header mesajım")); //1. parametre exchange adı, ikinci parametre routekey, 3.parametre header, 4.parametre mesajın içeriği

            //Console.WriteLine("Mesaj gönderilmiştir.");

            //Console.ReadLine();







            //COMPLEX TYPLEARI MESAJ OLARAK İLETMEK
        
            var factory = new ConnectionFactory(); 

            factory.Uri = new Uri("amqps://cezgimih:Xo_fII9ov60oS1bqMUETJEKjNyAK-JC3@toad.rmq.cloudamqp.com/cezgimih"); 

            using var connection = factory.CreateConnection(); 

            var channel = connection.CreateModel(); 

            channel.ExchangeDeclare("header-exchange", durable: true, type: ExchangeType.Headers); 

            Dictionary<string, object> headers = new Dictionary<string, object>();
            headers.Add("format", "pdf");
            headers.Add("share", "a4");

            var properties = channel.CreateBasicProperties(); 
            properties.Headers = headers;

        properties.Persistent = true;

            var product = new Product() {  // Öncelikle nesne olusturulur 
            Id = 1,
            Name = "Kalem",
            Price = 100,
            Stock = 1
            };

            var productJsonString = JsonSerializer.Serialize(product); // Json formatına çevrilir 

            channel.BasicPublish("header-exchange", string.Empty, properties, Encoding.UTF8.GetBytes(productJsonString));  // Buradada gönderilir
            Console.WriteLine("Mesaj gönderilmiştir.");

            Console.ReadLine();

        }
    }
}
