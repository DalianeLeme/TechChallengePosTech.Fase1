namespace TechChallenge.Infrastructure.Messaging
{
    public interface IRabbitMQPublisher
    {
        Task Publish<T>(T message, string queueName);
    }
}
