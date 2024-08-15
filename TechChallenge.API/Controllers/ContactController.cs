using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TechChallenge.Application.Services;
using TechChallenge.Domain.Models.Requests;
using TechChallenge.Domain.Models.Responses;

namespace TechChallenge.API.Controllers
{
   [ApiController]
   [Route("[controller]")]
    public class Contacts : ControllerBase
    {
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

       public async Task<CreateContactResponse> CreateContact([FromBody] CreateContactRequest request)
       {
           var response = await _service.CreateContact(request);
           return response;
       }

       [HttpGet]
       [Route("GetAllContacts")]
       [SwaggerOperation(Summary = "Consult a list of contacts")]
       [ProducesResponseType(typeof(GetContactResponse), StatusCodes.Status200OK)]
       [ProducesResponseType(typeof(GetContactResponse), StatusCodes.Status400BadRequest)]
       [ProducesResponseType(typeof(GetContactResponse), StatusCodes.Status500InternalServerError)]
       public async Task<GetContactResponse> GetContacts([FromQuery] GetContactRequest request)
       {
            var response = await _service.GetContact(request);
            return response;
       }

       [HttpPut]
       [Route("Update")]
       [SwaggerOperation(Summary = "Update a contact")]
       [ProducesResponseType(typeof(UpdateContactResponse), StatusCodes.Status201Created)]
       [ProducesResponseType(typeof(UpdateContactResponse), StatusCodes.Status400BadRequest)]
       [ProducesResponseType(typeof(UpdateContactResponse), StatusCodes.Status500InternalServerError)]
       public async Task<UpdateContactResponse> UpdateContact([FromBody] UpdateContactRequest request)
       {
            var response = await _service.UpdateContact(request);
            return response;
       }

        [HttpDelete]
        [Route("Delete/{id}")]
        [SwaggerOperation(Summary = "Delete a contact")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteContact(Guid id)
        {
            if (_service.DeleteContact(id))
                return Ok();

            return NoContent();
        }
    }
 }