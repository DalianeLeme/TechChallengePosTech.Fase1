﻿using TechChallenge.Domain.Models.Base;
using TechChallenge.Domain.Models.Requests;
using TechChallenge.Domain.Models.Responses;
using TechChallenge.Infrastructure.Context;
using TechChallenge.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace TechChallenge.Application.Services
{
    public class ContactService : IContactService
    {
        public ContactDbContext _context { get; set; }

        public ContactService(ContactDbContext context)
        {
            _context = context;
        }

        public Task<CreateContactResponse> CreateContact(CreateContactRequest request)
        {
            var ddd = _context.DDDs.FirstOrDefault(ddd => ddd.DDDCode == request.DDD);

            var contact = new Contact(
                Guid.NewGuid(),
                request.Name,
                request.Email,
                request.Phone,
                ddd,
                ddd.DDDId
                );

            _context.Add(contact);
            _context.SaveChanges();

            var response = new CreateContactResponse
            (
                contact.ContactId,
                contact.Name,
                contact.Email,
                contact.Ddd.DDDCode,
                contact.Phone
            );

            return Task.FromResult(response);
        }

        public Task<GetContactResponse> GetContact(GetContactRequest request)
        {
            //if(request.DDD != null)
            //{
            //    var contactsDb = _context.Contacts.Where(c => c.Ddd.DDDCode == request.DDD).ToList();
            //}
            var contactsDb = _context.Contacts.ToList();

            if(request.DDD != null)
                contactsDb = contactsDb.Where(c => c.Ddd.DDDCode == request.DDD).ToList();

            var contacts = new List<BaseResponse>();

            foreach (var contact in contactsDb)
            {
                contacts.Add(new BaseResponse(contact.ContactId, contact.Name, contact.Email, contact.Ddd.DDDCode, contact.Phone));
            }

            return Task.FromResult(new GetContactResponse(contacts));
        }

        public Task<UpdateContactResponse> UpdateContact(UpdateContactRequest request)
        {
            //var contactDb = _context.Contacts.Find(request.Id);
            var contactDb = _context.Contacts.Include(d => d.Ddd).First(c => c.ContactId == request.Id);

            if (contactDb == null)
                throw new Exception("Contact not found");

            if (request.DDD != contactDb.Ddd.DDDCode)
            {
                var ddd = _context.DDDs.FirstOrDefault(ddd => ddd.DDDCode == request.DDD);
                contactDb.Ddd = ddd;
            }

            contactDb.Name = request.Name;
            contactDb.Email = request.Email;
            contactDb.Phone = request.Phone;


            _context.Contacts.Update(contactDb);
            _context.SaveChanges();

            var response = new UpdateContactResponse
            (
                contactDb.ContactId,
                contactDb.Name,
                contactDb.Email,
                contactDb.Ddd.DDDCode,
                contactDb.Phone
            );

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
