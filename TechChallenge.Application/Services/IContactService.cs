using TechChallenge.Domain.Models.Base;
using TechChallenge.Domain.Models.Requests;
using TechChallenge.Domain.Models.Responses;

namespace TechChallenge.Application.Services
{
    public interface IContactService
    {
        Task<CreateContactResponse> CreateContact(CreateContactRequest request);
        Task<GetContactResponse> GetContact(int? ddd);
        Task<UpdateContactResponse> UpdateContact(UpdateContactRequest request);
        void DeleteContact(Guid id);
    }
}
