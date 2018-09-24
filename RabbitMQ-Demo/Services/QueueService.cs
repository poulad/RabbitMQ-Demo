using System.Text;
using Demo.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Demo.Services
{
    public class QueueService : IQueueService
    {
        private readonly RabbitMqOptions _options;

        public QueueService(IOptions<RabbitMqOptions> options)
        {
            _options = options.Value;
        }

        public void Publish(object message)
        {
            var factory = new ConnectionFactory
            {
                HostName = _options.Host,
                Port = _options.Port,
                UserName = _options.User,
                Password = _options.Password,
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "demo",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                string json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "demo",
                    basicProperties: null,
                    body: body
                );
            }
        }

        public T Dequeue<T>()
        {
            var factory = new ConnectionFactory
            {
                HostName = _options.Host,
                Port = _options.Port,
                UserName = _options.User,
                Password = _options.Password,
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "demo",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var consumer = new QueueingBasicConsumer(channel);

                channel.BasicConsume(
                    queue: "demo",
                    autoAck: true,
                    consumer: consumer
                );

                consumer.Queue.Dequeue(1_000, out var args);
                if (args == null)
                {
                    return default;
                }

                string json = Encoding.UTF8.GetString(args.Body);
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
    }
}