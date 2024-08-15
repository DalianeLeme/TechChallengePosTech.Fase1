using TechChallenge.Domain.Models.Base;
using TechChallenge.Domain.Models.Requests;
using TechChallenge.Domain.Models.Responses;

namespace TechChallenge.Application.Services
{
    public interface IContactService
    {
        Task<CreateContactResponse> CreateContact(CreateContactRequest contact);
        Task<GetContactResponse> GetContact(GetContactRequest contact);
        Task<UpdateContactResponse> UpdateContact(Guid id, UpdateContactRequest contact);
        bool DeleteContact(Guid id);
    }
}
