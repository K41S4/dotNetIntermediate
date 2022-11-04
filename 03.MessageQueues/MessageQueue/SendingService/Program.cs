using RabbitMQ.Client;

namespace SendingService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory { HostName = Common.Constants.Host };

            using var connection = connectionFactory.CreateConnection();
            using var model = connection.CreateModel();

            model.ExchangeDeclare(Common.Constants.QueueName, ExchangeType.Fanout);

            Console.WriteLine("Input service is started. Listening to folder");

            using (var folderWatcher = new InputListener(model, Common.Constants.FileFormat))
            {
                Console.ReadLine();
            }
        }
    }
}