using Microsoft.AspNetCore.Mvc;
using Prometheus;
using Swashbuckle.AspNetCore.Annotations;
using TechChallenge.Application.Services;
using TechChallenge.Application.Validators;
using TechChallenge.Domain.Models.Requests;
using TechChallenge.Domain.Models.Responses;

namespace TechChallenge.API.Controllers
{
    [ApiController]
   [Route("[controller]")]
    public class Contacts : ControllerBase
    {
       private static readonly Counter httpRequestCounter = Metrics.CreateCounter(
           "contacts_http_requests_total",
           "Contagem de requisições HTTP recebidas por status e método.",
           new CounterConfiguration
           {
               LabelNames = new[] { "status_code", "method", "path" }
           }
       );

       private readonly IContactService _service;
       public Contacts(IContactService service)
       {
            _service = service;
       }

        [HttpPost]
        [Route("Create")]
        [SwaggerOperation(Summary = "Create a contact")]
        [ProducesResponseType(typeof(CreateContactResponse), StatusCodes.Status201Created)]
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

            var response = await _service.CreateContact(request);
            httpRequestCounter.WithLabels("201", "POST", "/contacts/create").Inc();
            return Created("Index", response);
        }

       [HttpGet]
       [Route("GetAllContacts")]
       [SwaggerOperation(Summary = "Consult a list of contacts")]
       [ProducesResponseType(typeof(GetContactResponse), StatusCodes.Status200OK)]
       [ProducesResponseType(typeof(GetContactResponse), StatusCodes.Status400BadRequest)]
       [ProducesResponseType(typeof(GetContactResponse), StatusCodes.Status500InternalServerError)]
       public async Task<IActionResult> GetContacts([FromQuery] int? ddd)
       {
            var response = await _service.GetContact(ddd);
            httpRequestCounter.WithLabels("200", "GET", "/contacts/getallcontacts").Inc();
            return Ok(response);
       }

       [HttpPut]
       [Route("Update")]
       [SwaggerOperation(Summary = "Update a contact")]
       [ProducesResponseType(typeof(UpdateContactResponse), StatusCodes.Status200OK)]
       [ProducesResponseType(typeof(UpdateContactResponse), StatusCodes.Status400BadRequest)]
       [ProducesResponseType(typeof(UpdateContactResponse), StatusCodes.Status404NotFound)]
       [ProducesResponseType(typeof(UpdateContactResponse), StatusCodes.Status500InternalServerError)]
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
                    
                var response = await _service.UpdateContact(request);
                httpRequestCounter.WithLabels("200", "PUT", "/contacts/update").Inc();
                return Ok(response);
            }
            catch(Exception ex) 
            {
                httpRequestCounter.WithLabels("404", "PUT", "/contacts/update").Inc();
                return NotFound(ex.Message);
            }
       }

        [HttpDelete]
        [Route("Delete/{id}")]
        [SwaggerOperation(Summary = "Delete a contact")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteContact(Guid id)
        {
            try
            {
                 _service.DeleteContact(id);
                httpRequestCounter.WithLabels("200", "DELETE", "/contacts/delete").Inc();
                return Ok();
            }
            catch(Exception ex)
            {
                httpRequestCounter.WithLabels("404", "DELETE", "/contacts/delete").Inc();
                return NotFound(ex.Message);
            }
        }
    }
 }