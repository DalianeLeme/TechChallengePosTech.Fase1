using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TechChallenge.Application.Services;
using TechChallenge.Domain.Models.Requests;
using TechChallenge.Domain.Models.Responses;

public class CreateRabbitMQConsumer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CreateRabbitMQConsumer> _logger;
    private readonly string _hostname = "localhost";
    private readonly string _queueName = "create_contact_queue";
    private static CreateContactResponse? _lastCreatedContact;

    public CreateRabbitMQConsumer(IServiceProvider serviceProvider, ILogger<CreateRabbitMQConsumer> logger)
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
                var request = JsonSerializer.Deserialize<CreateContactRequest>(message);
                if (request != null)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var contactService = scope.ServiceProvider.GetRequiredService<IContactService>();

                    // Salva o contato e obtém a resposta completa
                    var createdContact = await contactService.CreateContact(request);

                    // Atualiza o último contato criado
                    _lastCreatedContact = createdContact;
                    _logger.LogInformation($"Último contato criado atualizado: {JsonSerializer.Serialize(_lastCreatedContact)}");

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

    public CreateContactResponse? GetLastCreatedContact()
    {
        return _lastCreatedContact;
    }
}
