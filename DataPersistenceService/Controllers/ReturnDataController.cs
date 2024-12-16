using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("persistenceData")]
public class ReturnDataController : ControllerBase
{
    private readonly CreateRabbitMQConsumer _createConsumer;
    private readonly UpdateRabbitMQConsumer _updateConsumer;
    private readonly GetRabbitMQConsumer _getConsumer;

    public ReturnDataController(
        CreateRabbitMQConsumer createConsumer,
        UpdateRabbitMQConsumer updateConsumer,
        GetRabbitMQConsumer getConsumer)
    {
        _createConsumer = createConsumer;
        _updateConsumer = updateConsumer;
        _getConsumer = getConsumer;
    }

    [HttpGet("create")]
    public IActionResult GetCreatedContacts()
    {
        var lastCreated = _createConsumer.GetLastCreatedContact();
        if (lastCreated == null)
            return NotFound("Nenhum contato criado encontrado.");
        return Ok(lastCreated);
    }

    [HttpGet("update")]
    public IActionResult GetUpdatedContacts()
    {
        var lastUpdated = _updateConsumer.GetLastUpdatedContact();
        if (lastUpdated == null)
            return NotFound("Nenhum contato atualizado encontrado.");
        return Ok(lastUpdated);
    }

    [HttpGet("get")]
    public IActionResult GetFetchedContacts()
    {
        try
        {
            var data = _getConsumer.GetProcessedData();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter dados recuperados: {ex.Message}");
        }
    }
}
