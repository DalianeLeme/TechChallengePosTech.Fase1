using TechChallenge.Domain.Models.Requests;
using TechChallenge.Domain.Models.Responses;

namespace TechChallenge.Application.Interfaces
{
    public interface IContactService
    {
        Task<CreateContactResponse> CreateContact(CreateContactRequest request);
    }
}
