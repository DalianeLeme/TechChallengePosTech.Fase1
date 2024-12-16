﻿using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TechChallenge.Infrastructure.Messaging;

namespace DeleteContactService.Messaging
{
    public class RabbitMQDeletePublisher : IRabbitMQPublisher
    {
        private readonly string _hostname = "localhost";

        public async Task Publish<T>(T message, string queueName)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            // Declaração da fila
            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // Serialização da mensagem para JSON
            var serializedMessage = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(serializedMessage);

            // Publicação na fila
            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: queueName,
                body: body);

            Console.WriteLine($"Mensagem publicada na fila {queueName}: {serializedMessage}");
        }
    }
}
