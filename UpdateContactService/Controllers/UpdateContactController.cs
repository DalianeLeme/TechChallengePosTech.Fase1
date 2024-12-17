using Microsoft.AspNetCore.Mvc;
using Prometheus;
using TechChallenge.Application.Validators;
using TechChallenge.Domain.Models.Requests;
using TechChallenge.Infrastructure.Messaging;

namespace UpdateContactService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateContactController : ControllerBase
    {
        private static readonly Counter httpRequestCounter = Metrics.CreateCounter(
            "contacts_update_request_total",
            "Contagem de requisições HTTP de update de contato.",
            new CounterConfiguration
            {
                LabelNames = new[] { "status_code", "method", "path" }
            }
        );

        private readonly IRabbitMQPublisher _publisher;
        public UpdateContactController(IRabbitMQPublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpPut]
        [Route("Update")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation(Summary = "Update a contact")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateContact([FromBody] UpdateContactRequest request)
        {
            try
            {
                var validator = new UpdateContactRequestValidator();
                var requestValidation = validator.Validate(request);

                if (!requestValidation.IsValid)
                {
                    httpRequestCounter.WithLabels("400", "PUT", "/contacts/update").Inc();
                    return BadRequest(requestValidation.Errors);
                }

                await _publisher.Publish(request, "update_contact_queue");

                httpRequestCounter.WithLabels("202", "PUT", "/contacts/update").Inc();

                return Accepted("Update request sent to the queue.");
            }
            catch (Exception ex)
            {
                httpRequestCounter.WithLabels("500", "PUT", "/contacts/update").Inc();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
