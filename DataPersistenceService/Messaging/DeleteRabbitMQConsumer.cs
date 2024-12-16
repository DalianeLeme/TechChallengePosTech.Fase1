using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TechChallenge.Application.Services;

public class DeleteRabbitMQConsumer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DeleteRabbitMQConsumer> _logger;
    private readonly string _hostname = "localhost";
    private readonly string _queueName = "delete_contact_queue";

    public DeleteRabbitMQConsumer(IServiceProvider serviceProvider, ILogger<DeleteRabbitMQConsumer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartConsumingAsync()
    {
        var retryPolicy = Policy.Handle<Exception>().RetryAsync(3);
        var circuitBreakerPolicy = Policy.Handle<Exception>().CircuitBreakerAsync(
            exceptionsAllowedBeforeBreaking: 2,
            durationOfBreak: TimeSpan.FromSeconds(10));

        var combinedPolicy = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);

        try
        {
            await combinedPolicy.ExecuteAsync(async () =>
            {
                await StartConsumingInternalAsync();
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao executar o consumidor: {ex.Message}");
        }
    }

    private async Task StartConsumingInternalAsync()
    {
        try
        {
            _logger.LogInformation("Inicializando o consumidor...");

            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                RequestedHeartbeat = TimeSpan.FromSeconds(30)
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    _logger.LogInformation($"Mensagem recebida da fila {_queueName}: {message}");

                    // Processar mensagem como Guid
                    var contactId = JsonSerializer.Deserialize<Guid>(message);
                    if (contactId != Guid.Empty)
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var contactService = scope.ServiceProvider.GetRequiredService<IContactService>();

                        _logger.LogInformation($"Processando exclusão do contato: {contactId}");
                        await contactService.DeleteContact(contactId);

                        _logger.LogInformation($"Contato {contactId} excluído com sucesso.");
                        await channel.BasicAckAsync(ea.DeliveryTag, false);
                    }
                    else
                    {
                        _logger.LogWarning("Mensagem inválida: GUID vazio.");
                        await channel.BasicNackAsync(ea.DeliveryTag, false, false);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erro ao processar mensagem: {ex.Message}");
                    await channel.BasicNackAsync(ea.DeliveryTag, false, false);
                }
            };

            await channel.BasicConsumeAsync(
                queue: _queueName,
                autoAck: false,
                consumer: consumer);

            _logger.LogInformation($"Consumidor iniciado na fila {_queueName}");

            await Task.Delay(Timeout.Infinite);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao inicializar consumidor: {ex.Message}");
            throw;
        }
    }
}
