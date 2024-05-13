using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace UdemyRabbitMQ.subcriber
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

            ////publisher ve subcriber tarafında olusturulan kuyruklar aynı olmalıdır yoıksa hata verir. Eğer publisher tarafında kuyruk olusturduysak burada olusturmamıza gerek yok
            ////channel.QueueDeclare("hello-queue", true, false, false); //ikinci parametre false veya true olması verilerin memoryde yada fiziksel olarak tutulması true yaparsak fiziksel tutulur ve rabbitmq'a restart atsak dahi veriler silinmez. //üçüncü parametre ise buradaki kuyruğa farklı bir kanal üstünden baglanmaya sağlar.dördüncü parametre ise subcriber down olursa kuyruk silinmemsini sağlar kuyruk işleme devam etsin. 


            //channel.BasicQos(0, 1, false); // 1.parametre boyut herhangi bir boyuttaki mesajı gönderebilirsin. 2.parametre kaç kaç mesaj gelsin. 3.parametre değer global olsunmu yani her bir subcribera eğer 2. parametre 6 seçildiyse ve 3.parametre true ise herbir sucribera 2 ser 2ser gönderir. Eğer false ise herbir sucribera 6'sar 6'sar gönderir.
            //var consumer = new EventingBasicConsumer(channel);

            //channel.BasicConsume("hello-queue",false,consumer); // dinleyeceği kuyruğu belirtiyoruz. // ikinci parametre true yaparsan rabbitmq kuyruktan bir mesaj gönderdiğinde yanlısda dogruda gönderse siler, false yaparsan silmez

            //consumer.Received += (object? sender, BasicDeliverEventArgs e) => {  //RabbitMq subcribera mesaj gönderdiğinde buradaki event fırlar ve burada yakalarız.
            //    var message = Encoding.UTF8.GetString(e.Body.ToArray());

            //    Thread.Sleep(1500);
            //    Console.WriteLine("Gelen Mesaj : " + message);

            //    channel.BasicAck(e.DeliveryTag,false);//işlem başarılıysa işlemi siliyor. // 2. parametre memoryde işlenmiş ama rabbitmq gitmemiş mesajları rabbitmq a haberdar eder.
            //};
            //Console.ReadLine();






            //FANOUT EXCHANGE
            //KUYRUGU PUBLİSHER OLARAK OLUSTURMAMA. SUCRIBER TARAFINDA OLUSTURULUR. KUYRUGUN ISMI ÖNEMLİ DEĞİLDİR.

            //var factory = new ConnectionFactory();

            //factory.Uri = new Uri("amqps://cezgimih:Xo_fII9ov60oS1bqMUETJEKjNyAK-JC3@toad.rmq.cloudamqp.com/cezgimih");


            //using var connection = factory.CreateConnection();


            //var channel = connection.CreateModel();

            ////publisher tarafıdna olusturdugumuz için burada olusturmamıza gerek yok. O yüzden yoruma alındı
            ////channel.ExchangeDeclare("logs-fanout", durable: true, type: ExchangeType.Fanout);  // Exchange olusturuyoruz. 1.paramatre exchange adı. 2.parametre uygulama restartta atsa exchange kaybolmasın.

            //var randomQueueName = "log-database-save-queue"; /*channel.QueueDeclare().QueueName;*/ //random kuyruk ismi oluşturmaya yarıyor.


            //channel.QueueDeclare(randomQueueName, true, false, false); // 1. parametre kuyruk ismi. 2.parametre fiziksel olarak sabit diskte kaydedilsin. 3. parametre başka kanallardan baglanılsınmı. 4.parametre down olunursa kuyruk silinsinmi

            //channel.QueueBind(randomQueueName, "logs-fanout", "", null); // Kuyruk declare etmek yerine channel üstünden kuyrugu exchange baglıyoruz. Bunun sebebi Consumer yani subcriber çıkış yaptıgında kuyrukta silinsin



            //channel.BasicQos(0, 1, false); // 1.parametre boyut herhangi bir boyuttaki mesajı gönderebilirsin. 2.parametre kaç kaç mesaj gelsin. 3.parametre değer global olsunmu yani her bir subcribera eğer 2. parametre 6 seçildiyse ve 3.parametre true ise herbir sucribera 2 ser 2ser gönderir. Eğer false ise herbir sucribera 6'sar 6'sar gönderir.

            //var consumer = new EventingBasicConsumer(channel);

            //channel.BasicConsume(randomQueueName, false, consumer); // dinleyeceği kuyruğu belirtiyoruz. // ikinci parametre true yaparsan rabbitmq kuyruktan bir mesaj gönderdiğinde yanlısda dogruda gönderse siler, false yaparsan silmez

            //Console.WriteLine("Loglar Dinleniyor");

            //consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
            //{  //RabbitMq subcribera mesaj gönderdiğinde buradaki event fırlar ve burada yakalarız.
            //    var message = Encoding.UTF8.GetString(e.Body.ToArray());

            //    Console.WriteLine("Gelen Mesaj : " + message);

            //    channel.BasicAck(e.DeliveryTag, false); //işlem başarılıysa işlemi siliyor. // 2. parametre memoryde işlenmiş ama rabbitmq gitmemiş mesajları rabbitmq a haberdar eder.
            //};

            //Console.ReadLine();









            ////DIRECT EXCHANGE
            //var factory = new ConnectionFactory();

            //factory.Uri = new Uri("amqps://cezgimih:Xo_fII9ov60oS1bqMUETJEKjNyAK-JC3@toad.rmq.cloudamqp.com/cezgimih");


            //using var connection = factory.CreateConnection();


            //var channel = connection.CreateModel();

            ////publisher tarafıdna olusturdugumuz için burada olusturmamıza gerek yok. O yüzden yoruma alındı
            ////channel.ExchangeDeclare("logs-fanout", durable: true, type: ExchangeType.Fanout);  // Exchange olusturuyoruz. 1.paramatre exchange adı. 2.parametre uygulama restartta atsa exchange kaybolmasın.


            //channel.BasicQos(0, 1, false); // 1.parametre boyut herhangi bir boyuttaki mesajı gönderebilirsin. 2.parametre kaç kaç mesaj gelsin. 3.parametre değer global olsunmu yani her bir subcribera eğer 2. parametre 6 seçildiyse ve 3.parametre true ise herbir sucribera 2 ser 2ser gönderir. Eğer false ise herbir sucribera 6'sar 6'sar gönderir.

            //var consumer = new EventingBasicConsumer(channel);

            //var queueName = "direct-queue-Ciritical";

            //channel.BasicConsume(queueName, false, consumer); // dinleyeceği kuyruğu belirtiyoruz. // ikinci parametre true yaparsan rabbitmq kuyruktan bir mesaj gönderdiğinde yanlısda dogruda gönderse siler, false yaparsan silmez

            //Console.WriteLine("Loglar Dinleniyor");

            //consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
            //{  //RabbitMq subcribera mesaj gönderdiğinde buradaki event fırlar ve burada yakalarız.
            //    var message = Encoding.UTF8.GetString(e.Body.ToArray());

            //    Thread.Sleep(1000);
            //    Console.WriteLine("Gelen Mesaj : " + message);

            //    //File.AppendAllText("log-critical.txt", message + "\n");

            //    channel.BasicAck(e.DeliveryTag, false); //işlem başarılıysa işlemi siliyor. // 2. parametre memoryde işlenmiş ama rabbitmq gitmemiş mesajları rabbitmq a haberdar eder.
            //};

            //Console.ReadLine();









            ////TOPIC EXCHANGE
            //var factory = new ConnectionFactory();

            //factory.Uri = new Uri("amqps://cezgimih:Xo_fII9ov60oS1bqMUETJEKjNyAK-JC3@toad.rmq.cloudamqp.com/cezgimih");


            //using var connection = factory.CreateConnection();


            //var channel = connection.CreateModel();

            ////publisher tarafıdna olusturdugumuz için burada olusturmamıza gerek yok. O yüzden yoruma alındı

            //channel.BasicQos(0, 1, false); // 1.parametre boyut herhangi bir boyuttaki mesajı gönderebilirsin. 2.parametre kaç kaç mesaj gelsin. 3.parametre değer global olsunmu yani her bir subcribera eğer 2. parametre 6 seçildiyse ve 3.parametre true ise herbir sucribera 2 ser 2ser gönderir. Eğer false ise herbir sucribera 6'sar 6'sar gönderir.

            //var consumer = new EventingBasicConsumer(channel);

            //var queueName = channel.QueueDeclare().QueueName;
            //var routekey = "Info.#";//Başı Info olsun sonu önemli değil   //"*.*.Warning";//Sonu Warning olanlar gelsin             //"*.Error.*"; // Ortasıda error olan başı sonu önemli değil.

            //channel.QueueBind(queueName,"logs-topic",routekey);

            //channel.BasicConsume(queueName, false, consumer); // dinleyeceği kuyruğu belirtiyoruz. // ikinci parametre true yaparsan rabbitmq kuyruktan bir mesaj gönderdiğinde yanlısda dogruda gönderse siler, false yaparsan silmez

            //Console.WriteLine("Loglar Dinleniyor");

            //consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
            //{  //RabbitMq subcribera mesaj gönderdiğinde buradaki event fırlar ve burada yakalarız.
            //    var message = Encoding.UTF8.GetString(e.Body.ToArray());

            //    Thread.Sleep(1000);
            //    Console.WriteLine("Gelen Mesaj : " + message);

            //    //File.AppendAllText("log-critical.txt", message + "\n");

            //    channel.BasicAck(e.DeliveryTag, false); //işlem başarılıysa işlemi siliyor. // 2. parametre memoryde işlenmiş ama rabbitmq gitmemiş mesajları rabbitmq a haberdar eder.
            //};

            //Console.ReadLine();









            //HEADER EXCHANGE
            var factory = new ConnectionFactory();

            factory.Uri = new Uri("amqps://cezgimih:Xo_fII9ov60oS1bqMUETJEKjNyAK-JC3@toad.rmq.cloudamqp.com/cezgimih");


            using var connection = factory.CreateConnection();


            var channel = connection.CreateModel();

            //publisher tarafıdna olusturdugumuz için burada olusturmamıza gerek yok. O yüzden yoruma alındı

            channel.BasicQos(0, 1, false); // 1.parametre boyut herhangi bir boyuttaki mesajı gönderebilirsin. 2.parametre kaç kaç mesaj gelsin. 3.parametre değer global olsunmu yani her bir subcribera eğer 2. parametre 6 seçildiyse ve 3.parametre true ise herbir sucribera 2 ser 2ser gönderir. Eğer false ise herbir sucribera 6'sar 6'sar gönderir.

            var consumer = new EventingBasicConsumer(channel);

            var queueName = channel.QueueDeclare().QueueName;

            Dictionary<string,object> headers = new Dictionary<string, object>();
            headers.Add("format", "pdf");
            headers.Add("share", "a4");
            headers.Add("x-match", "all"); // all ifadesi string ve value çiftrinini gelen işlemde eşlenmesi gerektiğini belirtir.
            //headers.Add("x-match", "any"); // any ifadesi string ve value çiftinden herhangi birinin uymasının yeterli olmasıdır.

            channel.QueueBind(queueName, "header-exchange",string.Empty,headers);



            channel.BasicConsume(queueName, false, consumer); // dinleyeceği kuyruğu belirtiyoruz. // ikinci parametre true yaparsan rabbitmq kuyruktan bir mesaj gönderdiğinde yanlısda dogruda gönderse siler, false yaparsan silmez

            Console.WriteLine("Loglar Dinleniyor");

            consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
            {  //RabbitMq subcribera mesaj gönderdiğinde buradaki event fırlar ve burada yakalarız.
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                Thread.Sleep(1000);
                Console.WriteLine("Gelen Mesaj : " + message);


                channel.BasicAck(e.DeliveryTag, false); //işlem başarılıysa işlemi siliyor. // 2. parametre memoryde işlenmiş ama rabbitmq gitmemiş mesajları rabbitmq a haberdar eder.
            };

            Console.ReadLine();












            //NOOT : Exchange poublisher tarafında oluşturuyorsak önce publisherı ayağa kaldırmamız gerekiyorki kuyrugun baglanabileceği bir exchange olsun. Exchange'i istersek subcriber tarafındada oluşturabiliriz.

        }


    }
}
