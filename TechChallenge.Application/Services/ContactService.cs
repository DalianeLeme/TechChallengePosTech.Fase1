using TechChallenge.Application.Interfaces;
using TechChallenge.Domain.Models.Requests;
using TechChallenge.Domain.Models.Responses;
using TechChallenge.Infrastructure.Context;

namespace TechChallenge.Application.Services
{
    public class ContactService : IContactService
    {
        public ContactDbContext _context { get; set; }
        
        public ContactService(ContactDbContext context)
        {
            _context = context;
        }

        public async Task<CreateContactResponse> CreateContact(CreateContactRequest contact)
        {
            _context.Add(contact);
            _context.SaveChanges();

            return new CreateContactResponse
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                DDD = contact.DDD,
                Phone = contact.Phone
            };
        }
    }
}
