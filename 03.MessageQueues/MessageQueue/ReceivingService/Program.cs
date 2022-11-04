using System;
using System.IO;
using Common;
using Common.Messages;
//using Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace ReceivingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FileOperatingService.CreateDirectoryIfNeeded(Common.Constants.ReceiveFolder);

            var factory = new ConnectionFactory { HostName = Common.Constants.Host };

            using var connection = factory.CreateConnection();
            using var model = connection.CreateModel();

            model.ExchangeDeclare(Common.Constants.QueueName, ExchangeType.Fanout);

            var queueName = model.QueueDeclare().QueueName;
            model.QueueBind(queueName, Common.Constants.QueueName, "");

            var consumer = new EventingBasicConsumer(model);
            consumer.Received += ReceivingHandler.Received;

            model.BasicConsume(queueName, true, consumer);

            Console.WriteLine("Receiving service is started. Listening to queue");

            Console.ReadLine();
        }
    }
}