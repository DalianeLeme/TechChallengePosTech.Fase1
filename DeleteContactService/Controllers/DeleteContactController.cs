using Microsoft.AspNetCore.Mvc;
using Prometheus;
using Swashbuckle.AspNetCore.Annotations;
using TechChallenge.Infrastructure.Messaging;

namespace DeleteContactService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteContactController : ControllerBase
    {
        private static readonly Counter httpRequestCounter = Metrics.CreateCounter(
            "contacts_delete_request_total",
            "Contagem de requisições HTTP de update de contato.",
            new CounterConfiguration
            {
                LabelNames = new[] { "status_code", "method", "path" }
            }
        );

        private readonly IRabbitMQPublisher _publisher;
        public DeleteContactController(IRabbitMQPublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [SwaggerOperation(Summary = "Delete a contact")]
        [ProducesResponseType(typeof(string), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            try
            {
                // Verifica se o ID é válido
                if (id == Guid.Empty)
                {
                    httpRequestCounter.WithLabels("400", "DELETE", "/contacts/delete").Inc();
                    return BadRequest("Invalid ID provided.");
                }

                // Publica a solicitação de exclusão na fila RabbitMQ
                await _publisher.Publish(id, "delete_contact_queue");

                // Incrementa a métrica de sucesso
                httpRequestCounter.WithLabels("202", "DELETE", "/contacts/delete").Inc();

                // Retorna aceitação da solicitação
                return Accepted("Delete request sent to the queue.");
            }
            catch (Exception ex)
            {
                // Incrementa a métrica de erro
                httpRequestCounter.WithLabels("500", "DELETE", "/contacts/delete").Inc();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}