using Azure;
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

        public Task<CreateContactResponse> CreateContact(CreateContactRequest contact)
        {
            _context.Add(contact);
            _context.SaveChanges();

            var response = new CreateContactResponse
            {
                Id = Guid.NewGuid(),
                Name = contact.Name,
                Email = contact.Email,
                DDD = contact.DDD,
                Phone = contact.Phone
            };

            return Task.FromResult(response);
        }

        public Task<GetContactResponse> GetContact(GetContactRequest contact)
        {
            var contacts = _context.Contacts.ToList();

            return Task.FromResult(contacts);
        }

        public Task<UpdateContactResponse> UpdateContact(UpdateContactRequest contact)
        {
            var contactDb = _context.Contacts.Find(contact.Id);

            if (contactDb == null)
                throw new Exception("Contact not found");

            contactDb.Name = contact.Name;
            contactDb.Email = contact.Email;
            contactDb.Ddd = contact.DDD;
            contactDb.Phone = contact.Phone;


            _context.Contacts.Update(contactDb);
            _context.SaveChanges();

            var response = new UpdateContactResponse
            {
                Id = contactDb.ContactId,
                Name = contactDb.Name,
                Email = contactDb.Email,
                DDD = contactDb.Ddd,
                Phone = contactDb.Phone
            };

            return Task.FromResult(response);
        }

        public bool DeleteContact(Guid id)
        {
            var contactDb = _context.Contacts.Find(id);

            if (contactDb == null)
                throw new Exception("Contact not found");

            _context.Remove(contactDb);
            _context.SaveChanges();

            return true;

        }
    }
}
