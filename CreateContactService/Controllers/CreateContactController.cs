using Microsoft.AspNetCore.Mvc;
using Prometheus;
using Swashbuckle.AspNetCore.Annotations;
using TechChallenge.Application.Validators;
using TechChallenge.Domain.Models.Requests;
using TechChallenge.Domain.Models.Responses;
using TechChallenge.Infrastructure.Messaging;

namespace CreateContactService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreateContactsController : ControllerBase
    {
        private static readonly Counter httpRequestCounter = Metrics.CreateCounter(
            "contacts_create_request_total",
            "Contagem de requisições HTTP recebidas para criação de contatos.",
            new CounterConfiguration
            {
                LabelNames = new[] { "status_code", "method", "path" }
            }
        );

        private readonly IRabbitMQPublisher _publisher;
        public CreateContactsController(IRabbitMQPublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpPost]
        [Route("Create")]
        [SwaggerOperation(Summary = "Create a contact")]
        [ProducesResponseType(typeof(CreateContactResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(CreateContactResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CreateContactResponse), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateContact([FromBody] CreateContactRequest request)
        {
            var validator = new CreateContactRequestValidator();
            var requestValidation = validator.Validate(request);
            if (!requestValidation.IsValid)
            {
                httpRequestCounter.WithLabels("400", "POST", "/contacts/create").Inc();
                return BadRequest(requestValidation.Errors);
            }

            await _publisher.Publish(request, "create_contact_queue");
            httpRequestCounter.WithLabels("201", "POST", "/contacts/create").Inc();
            return Accepted("Contact create request sent to the queue.");
        }
    }
}
