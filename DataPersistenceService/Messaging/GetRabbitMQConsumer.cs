using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TechChallenge.Application.Services;
using Polly;

public class GetRabbitMQConsumer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<GetRabbitMQConsumer> _logger;
    private readonly string _hostname = "localhost";
    private readonly string _inputQueueName = "get_contacts_queue";

    private readonly List<string> _processedData = new();
    public List<string> GetProcessedData() => _processedData;

    public GetRabbitMQConsumer(IServiceProvider serviceProvider, ILogger<GetRabbitMQConsumer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartConsumingAsync()
    {
        var retryPolicy = Policy.Handle<Exception>().RetryAsync(3, onRetry: (exception, retryCount) =>
        {
            _logger.LogWarning($"Retry {retryCount} devido a erro: {exception.Message}");
        });

        var circuitBreakerPolicy = Policy.Handle<Exception>().CircuitBreakerAsync(
            exceptionsAllowedBeforeBreaking: 2,
            durationOfBreak: TimeSpan.FromSeconds(10),
            onBreak: (exception, duration) =>
            {
                _logger.LogWarning($"Circuito aberto devido a erro: {exception.Message}. Reiniciará em {duration.TotalSeconds} segundos.");
            },
            onReset: () =>
            {
                _logger.LogInformation("Circuito fechado. Operações retomadas.");
            });

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
            _logger.LogError($"Erro crítico ao executar consumidor: {ex.Message}");
        }
    }

    private async Task StartConsumingInternalAsync()
    {
        _logger.LogInformation("Inicializando o consumidor...");

        var factory = new ConnectionFactory() { HostName = _hostname, RequestedHeartbeat = TimeSpan.FromSeconds(30) };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: _inputQueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _logger.LogInformation("Fila declarada.");

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var ddd = JsonSerializer.Deserialize<int?>(Encoding.UTF8.GetString(body));

            try
            {
                _logger.LogInformation("Mensagem recebida. Processando...");

                using var scope = _serviceProvider.CreateScope();
                var contactService = scope.ServiceProvider.GetRequiredService<IContactService>();
                var result = await contactService.GetContact(ddd);

                var processedMessage = JsonSerializer.Serialize(result);
                _processedData.Add(processedMessage);

                _logger.LogInformation("Mensagem processada e armazenada com sucesso.");
                await channel.BasicAckAsync(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar mensagem: {ex.Message}");
                await channel.BasicNackAsync(ea.DeliveryTag, false, false);
            }
        };

        await channel.BasicConsumeAsync(queue: _inputQueueName, autoAck: false, consumer: consumer);

        _logger.LogInformation($"Consumidor iniciado na fila: {_inputQueueName}");
        await Task.Delay(Timeout.Infinite);
    }
}
