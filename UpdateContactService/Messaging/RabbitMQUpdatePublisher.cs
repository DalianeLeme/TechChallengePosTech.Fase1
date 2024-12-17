using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using TechChallenge.Infrastructure.Messaging;

namespace UpdateContactService.Messaging
{
    public class RabbitMQUpdatePublisher : IRabbitMQPublisher
    {
        private readonly string _hostname = "localhost";  

        public async Task Publish<T>(T message, string queueName)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };

            using var connection = await factory.CreateConnectionAsync();  

            using var channel = await connection.CreateChannelAsync(); 
            await channel.QueueDeclareAsync(queue: queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var messageBody = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(messageBody);

            await channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body);
        }
    }
}
