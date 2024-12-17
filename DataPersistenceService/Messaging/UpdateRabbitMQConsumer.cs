using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TechChallenge.Application.Services;
using TechChallenge.Domain.Models.Requests;
using TechChallenge.Domain.Models.Responses;

public class UpdateRabbitMQConsumer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<UpdateRabbitMQConsumer> _logger;
    private readonly string _hostname = "localhost";
    private readonly string _queueName = "update_contact_queue";
    private static UpdateContactResponse? _lastUpdatedContact;

    public UpdateRabbitMQConsumer(IServiceProvider serviceProvider, ILogger<UpdateRabbitMQConsumer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartConsumingAsync()
    {
        var retryPolicy = Policy.Handle<Exception>().RetryAsync(3);
        var circuitBreakerPolicy = Policy.Handle<Exception>().CircuitBreakerAsync(2, TimeSpan.FromSeconds(10));
        var combinedPolicy = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);

        try
        {
            await combinedPolicy.ExecuteAsync(async () => await StartConsumingInternalAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao executar o consumidor: {ex.Message}");
        }
    }

    private async Task StartConsumingInternalAsync()
    {
        var factory = new ConnectionFactory() { HostName = _hostname };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(_queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            try
            {
                var request = JsonSerializer.Deserialize<UpdateContactRequest>(message);
                if (request != null)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var contactService = scope.ServiceProvider.GetRequiredService<IContactService>();

                    var updatedContact = await contactService.UpdateContact(request);

                    _lastUpdatedContact = updatedContact;
                    _logger.LogInformation($"Último contato atualizado: {JsonSerializer.Serialize(_lastUpdatedContact)}");

                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar mensagem: {ex.Message}");
                await channel.BasicNackAsync(ea.DeliveryTag, false, false);
            }
        };

        await channel.BasicConsumeAsync(_queueName, autoAck: false, consumer: consumer);
        await Task.Delay(Timeout.Infinite);
    }

    public UpdateContactResponse? GetLastUpdatedContact()
    {
        return _lastUpdatedContact;
    }
}
