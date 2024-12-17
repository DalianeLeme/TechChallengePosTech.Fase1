using Microsoft.AspNetCore.Mvc;
using Prometheus;
using Swashbuckle.AspNetCore.Annotations;
using TechChallenge.Infrastructure.Messaging;

namespace GetContactService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetContactsController : ControllerBase
    {
        private static readonly Counter httpRequestCounter = Metrics.CreateCounter(
            "contacts_get_request_total",
            "Contagem de requisições HTTP de get de contato.",
            new CounterConfiguration
            {
                LabelNames = new[] { "status_code", "method", "path" }
            }
        );

        private readonly IRabbitMQPublisher _publisher;
        public GetContactsController(IRabbitMQPublisher publisher)
        {
            _publisher = publisher;
        }


        [HttpGet]
        [Route("GetAllContacts")]
        [SwaggerOperation(Summary = "Consult a list of contacts")]
        [ProducesResponseType(typeof(string), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetContacts([FromQuery] int? ddd)
        {
            try
            {
                if (ddd.HasValue && (ddd < 11 || ddd > 99))
                {
                    httpRequestCounter.WithLabels("400", "GET", "/contacts/getallcontacts").Inc();
                    return BadRequest("Invalid DDD provided. It must be a two-digit number.");
                }

                await _publisher.Publish(ddd, "get_contacts_queue");

                httpRequestCounter.WithLabels("202", "GET", "/contacts/getallcontacts").Inc();

                return Accepted("Request to retrieve contacts sent for processing.");
            }
            catch (Exception ex)
            {
                httpRequestCounter.WithLabels("500", "GET", "/contacts/getallcontacts").Inc();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
