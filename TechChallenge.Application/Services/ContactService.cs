using TechChallenge.Application.Interfaces;
using TechChallenge.Domain.Models.Requests;
using TechChallenge.Domain.Models.Responses;
using TechChallenge.Infrastructure.Context;

namespace TechChallenge.Application.Services
{
    public class ContactService
    {
        //public ContactDbContext _context { get; set; }
        
        //public ContactService(ContactDbContext context)
        //{
        //    _context = context;
        //}

        //public Task<CreateContactResponse> CreateContact(CreateContactRequest contact)
        //{
        //    _context.Add(contact);
        //    _context.SaveChanges();

        //    return new CreateContactResponse
        //    {
        //        Id = contact.,
        //        Name = contact.Name,
        //        Email = contact.Email,
        //        DDD = contact.DDD,
        //        Phone = contact.Phone
        //    };
        //}
            
        //public Task<IList<GetContactResponse>> GetContact(GetContactRequest contact)
        //{
        //    var contactDb = _context.Contacts.ToList();

        //    return contactDb;
        //}

        //public Task<UpdateContactResponse> UpdateContact(Guid id, UpdateContactRequest contact)
        //{
        //    var contactDb = _context.Contacts.Find(id);

        //    if (contactDb == null)
        //        throw new Exception("Contact not found");

        //    contactDb.Name = contact.Name;
        //    contactDb.Email = contact.Email;
        //    contactDb.DDDId = contact.DDD;
        //    contactDb.Phone = contact.Phone;


        //    _context.Contacts.Update(contactDb);
        //    _context.SaveChanges();

        //    return new UpdateContactResponse
        //    {
        //        Id = contact.Id,
        //        Name = contact.Name,
        //        Email = contact.Email,
        //        DDD = contact.DDD,
        //        Phone = contact.Phone
        //    };
        //}

        //public Task<IActiontResult> DeleteContact(Guid id)
        //{
        //    var contactDb = _context.Contacts.Find(id);

        //    if (contactDb == null)
        //        throw new Exception("Contact not found");

        //    _context.Remove(contactDb);
        //    _context.SaveChanges();

        //    return ;

        //}

        //public Task<IList<GetContactResponse>> GetContactsByDDD()
        //{
        //    var contactDb = _context.Contacts.Find();

        //    if (contactDb.DDDId == null)
        //        throw new Exception("DDD not found");

        //    return contactDb;
        //}

    }
}
