using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TechChallenge.Application.Interfaces;
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
       public async Task<CreateContactResponse> CreateContact([FromBody] CreateContactRequest request)
       {
           var response = await _service.CreateContact(request);
           return response;
       }

       [HttpGet]
       public async IEnumerable<GetContactResponse> GetContacts()
       {
            var response = await _service.GetContact(request);
            return response;
       }

       [HttpPut]
       public async Task<UpdateContactResponse> UpdateContact([FromHeader] Guid id, [FromBody] UpdateContactRequest request)
       {
            var response = await _service.UpdateContact(id, request);
            return response;
       }

       [HttpDelete]
       public async Task<bool> DeleteContact(Guid id)
       {
            var response = await _service.DeleteContact(id);
            return response;
       }
    }
 }