using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using MessageQueues.Core;
using MessageQueues.Core.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageQueues.CentralHost
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    Console.WriteLine("Setup Thread: {0}", Thread.CurrentThread.ManagedThreadId);
                    channel.QueueDeclare(Queues.Files, durable: true, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (sender, eventArgs) =>
                    {
                        Console.WriteLine("Handler Thread: {0}", Thread.CurrentThread.ManagedThreadId);
                        var body = eventArgs.Body;

                        using (var memoryStream = new MemoryStream(body))
                        {
                            var binaryFormatter =new BinaryFormatter();
                            var message = binaryFormatter.Deserialize(memoryStream) as FileMessage;
                            Console.WriteLine("harvester: {0} fileName: {1} operationType: {2}", message.Harvester, message.FileName, message.OperationType);
                        }
                    };

                    channel.BasicConsume(Queues.Files, noAck: true, consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
