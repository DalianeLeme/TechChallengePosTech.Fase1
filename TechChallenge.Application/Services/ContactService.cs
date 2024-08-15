using Azure;
using TechChallenge.Domain.Models.Base;
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
            //_context.Add(contact);
            //_context.SaveChanges();
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
            var contact1 = new BaseResponse{
                Id = Guid.NewGuid(),
                Name = "Daniliane",
                Email = "odio@gmail.com",
                DDD = 11,
                Phone = 12345678
            };            
            
            var contact2 = new BaseResponse{
                Id = Guid.NewGuid(),
                Name = "Gaspar",
                Email = "odio@gmail.com",
                DDD = 19,
                Phone = 12345678
            };            
            
            var contact3 = new BaseResponse{
                Id = Guid.NewGuid(),
                Name = "sdfjalkdgh",
                Email = "odio@gmail.com",
                DDD = 25,
                Phone = 12345678
            };

            var listContacts = new List<BaseResponse>()
            { contact1, contact2, contact3 };

            var response = new GetContactResponse();
            response.Contacts = listContacts;

            return Task.FromResult(response);
        }

        public Task<UpdateContactResponse> UpdateContact(Guid id, UpdateContactRequest contact)
        {
            //var contactDb = _context.Contacts.Find(id);

            //if (contactDb == null)
            //    throw new Exception("Contact not found");

            //contactDb.Name = contact.Name;
            //contactDb.Email = contact.Email;
            //contactDb.DDDId = contact.DDD;
            //contactDb.Phone = contact.Phone;


            //_context.Contacts.Update(contactDb);
            //_context.SaveChanges();

            //return new UpdateContactResponse
            //{
            //    Id = contactDb.Id,
            //    Name = contactDb.Name,
            //    Email = contactDb.Email,
            //    DDD = contactDb.DDD,
            //    Phone = contactDb.Phone
            //};

            var response = new UpdateContactResponse
            {
                Id = id,
                Name = contact.Name,
                Email = contact.Email,
                DDD = contact.DDD,
                Phone = contact.Phone
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

        //public Task<IList<GetContactResponse>> GetContactsByDDD()
        //{
        //    var contactDb = _context.Contacts.Find();

        //    if (contactDb.DDDId == null)
        //        throw new Exception("DDD not found");

        //    return contactDb;
        //}

    }
}
