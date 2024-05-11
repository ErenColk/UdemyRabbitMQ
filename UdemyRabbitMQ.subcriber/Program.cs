using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace UdemyRabbitMQ.subcriber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();

            factory.Uri = new Uri("amqps://cezgimih:Xo_fII9ov60oS1bqMUETJEKjNyAK-JC3@toad.rmq.cloudamqp.com/cezgimih");


            using var connection = factory.CreateConnection();


            var channel = connection.CreateModel();

            //publisher ve subcriber tarafında olusturulan kuyruklar aynı olmalıdır yoıksa hata verir. Eğer publisher tarafında kuyruk olusturduysak burada olusturmamıza gerek yok
            //channel.QueueDeclare("hello-queue", true, false, false); //ikinci parametre false veya true olması verilerin memoryde yada fiziksel olarak tutulması true yaparsak fiziksel tutulur ve rabbitmq'a restart atsak dahi veriler silinmez. //üçüncü parametre ise buradaki kuyruğa farklı bir kanal üstünden baglanmaya sağlar.dördüncü parametre ise subcriber down olursa kuyruk silinmemsini sağlar kuyruk işleme devam etsin. 


            channel.BasicQos(0, 1, false); // 1.parametre boyut herhangi bir boyuttaki mesajı gönderebilirsin. 2.parametre kaç kaç mesaj gelsin. 3.parametre değer global olsunmu yani her bir subcribera eğer 2. parametre 6 seçildiyse ve 3.parametre true ise herbir sucribera 2 ser 2ser gönderir. Eğer false ise herbir sucribera 6'sar 6'sar gönderir.
            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume("hello-queue",false,consumer); // dinleyeceği kuyruğu belirtiyoruz. // ikinci parametre true yaparsan rabbitmq kuyruktan bir mesaj gönderdiğinde yanlısda dogruda gönderse siler, false yaparsan silmez

            consumer.Received += (object? sender, BasicDeliverEventArgs e) => {  //RabbitMq subcribera mesaj gönderdiğinde buradaki event fırlar ve burada yakalarız.
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                Thread.Sleep(1500);
                Console.WriteLine("Gelen Mesaj : " + message);

                channel.BasicAck(e.DeliveryTag,false);//işlem başarılıysa işlemi siliyor. // 2. parametre memoryde işlenmiş ama rabbitmq gitmemiş mesajları rabbitmq a haberdar eder.
            };
            Console.ReadLine();
        }


        //yukardaki 27.satıra yazıldı.
        //private static void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
