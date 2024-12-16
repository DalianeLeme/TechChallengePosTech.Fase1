using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using TechChallenge.Infrastructure.Messaging;

namespace UpdateContactService.Messaging
{
    public class RabbitMQUpdatePublisher : IRabbitMQPublisher
    {
        private readonly string _hostname = "localhost";  // Endereço do RabbitMQ

        public async Task Publish<T>(T message, string queueName)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };

            // Criação assíncrona da conexão
            using var connection = await factory.CreateConnectionAsync();  // A conexão é assíncrona

            // Criação do canal (IModel) a partir da conexão
            using var channel = await connection.CreateChannelAsync();  // IModel é o canal usado para se comunicar com o RabbitMQ

            // Declaração da fila (garante que a fila exista antes de publicar a mensagem)
            await channel.QueueDeclareAsync(queue: queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // Serialização da mensagem para o formato esperado
            var messageBody = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(messageBody);

            // Publicação da mensagem na fila
            await channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body);
        }
    }
}
