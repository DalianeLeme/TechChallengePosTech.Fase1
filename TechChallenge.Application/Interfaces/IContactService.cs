using TechChallenge.Domain.Models.Requests;
using TechChallenge.Domain.Models.Responses;

namespace TechChallenge.Application.Interfaces
{
    public interface IContactService
    {
        Task<CreateContactResponse> CreateContact(CreateContactRequest contact);
        Task<IList<GetContactResponse>> GetContact(GetContactRequest contact);
        Task<UpdateContactResponse> UpdateContact(Guid id, UpdateContactRequest contact);
        Task<IActionResult> DeleteContact(Guid id);
    }
}
