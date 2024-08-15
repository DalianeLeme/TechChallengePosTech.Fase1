using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TechChallenge.Application.Interfaces;
using TechChallenge.Domain.Models.Base;
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
       [Route("AllContacts")]
       [SwaggerOperation(Summary = "Consult a list of contacts")]
       [ProducesResponseType(typeof(GetContactResponse), StatusCodes.Status201Created)]
       [ProducesResponseType(typeof(GetContactResponse), StatusCodes.Status400BadRequest)]
       [ProducesResponseType(typeof(GetContactResponse), StatusCodes.Status500InternalServerError)]
       public async Task<IList<GetContactResponse>> GetContacts(GetContactRequest request)
       {
            var response = await _service.GetContact(request);
            return response;
       }

       [HttpPut]
       [Route("Update/{id}")]
       [SwaggerOperation(Summary = "Update a contact")]
       [ProducesResponseType(typeof(UpdateContactResponse), StatusCodes.Status201Created)]
       [ProducesResponseType(typeof(UpdateContactResponse), StatusCodes.Status400BadRequest)]
       [ProducesResponseType(typeof(UpdateContactResponse), StatusCodes.Status500InternalServerError)]
       public async Task<UpdateContactResponse> UpdateContact([FromHeader] Guid id, [FromBody] UpdateContactRequest request)
       {
            var response = await _service.UpdateContact(id, request);
            return response;
       }

       //[HttpDelete]
       //[Route("Delete/{id}")]
       //[SwaggerOperation(Summary = "Delete a contact")]
       //[ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
       //[ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
       //[ProducesResponseType(typeof(bool), StatusCodes.Status500InternalServerError)]
       //public async Task<IActionResult> DeleteContact(Guid id)
       //{
       //     var response = await _service.DeleteContact(id);
       //     return NoContent();
       //}
    }
 }