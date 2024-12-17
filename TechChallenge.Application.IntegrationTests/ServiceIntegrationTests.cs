using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using TechChallenge.Application.Services;
using TechChallenge.Domain.Models.Requests;
using TechChallenge.Infrastructure.Context;

namespace TechChallenge.Application.IntegrationTests
{
    public class ServiceIntegrationTests : IDisposable
    {
        private readonly ContactDbContext _context;
        private readonly ContactService _service;
        private readonly IDbContextTransaction _transaction;

        public ServiceIntegrationTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<ContactDbContext>()
                .UseSqlServer(configuration.GetConnectionString("ConexaoPadrao"))
                .Options;

            _context = new ContactDbContext(options);
            _service = new ContactService(_context);
            _transaction = _context.Database.BeginTransaction();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task CreateContact_ShouldAddContactToDatabase()
        {
            var request = new CreateContactRequest(
                "Dalia Leme",
                "dalia@exemplo.com",
                 11,
                "123456789"
            );

            var response = await _service.CreateContact(request);

            var contact = await _context.Contacts.Include(c => c.Ddd).FirstOrDefaultAsync(c => c.ContactId == response.Id);

            Assert.NotNull(contact);
            Assert.Equal(request.Name, contact.Name);
            Assert.Equal(request.Email, contact.Email);
            Assert.Equal(request.Phone, contact.Phone);
            Assert.Equal(request.DDD, contact.Ddd.DDDCode);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task GetContact_ShouldReturnAllContacts()
        {
            var response = await _service.GetContact(null);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Contacts);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task GetContact_WithDDDFilter_ShouldReturnFilteredContacts()
        {
            int dddToFilter = 11;

            var response = await _service.GetContact(dddToFilter);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Contacts);
            Assert.All(response.Contacts, contact => Assert.Equal(dddToFilter, contact.DDD));
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task UpdateContact_ShouldModifyContactInDatabase()
        {
            var contact = _context.Contacts.First();
            var request = new UpdateContactRequest(
               contact.ContactId,
               "Dalia Leme",
               "dalia@exemplo.com",
               21,
               "987654321"
            );

            var response = await _service.UpdateContact(request);

            var updatedContactInDb = await _context.Contacts.Include(c => c.Ddd).FirstOrDefaultAsync(c => c.ContactId == response.Id);

            Assert.NotNull(updatedContactInDb);
            Assert.Equal(request.Name, updatedContactInDb.Name);
            Assert.Equal(request.Email, updatedContactInDb.Email);
            Assert.Equal(request.Phone, updatedContactInDb.Phone);
            Assert.Equal(request.DDD, updatedContactInDb.Ddd.DDDCode);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task DeleteContact_ShouldRemoveContactFromDatabase()
        {
            var contact = await _context.Contacts.FirstAsync();

            await _service.DeleteContact(contact.ContactId);  

            var deletedContact = await _context.Contacts.AsNoTracking()
                .FirstOrDefaultAsync(c => c.ContactId == contact.ContactId);
            Assert.Null(deletedContact);
        }


        [Fact]
        [Trait("Category", "Integration")]
        public async Task DeleteContact_NonExistingContact_ShouldThrowException()
        {
            var nonExistingContactId = Guid.NewGuid();

            var exception = await Assert.ThrowsAsync<Exception>(() =>
                Task.Run(() => _service.DeleteContact(nonExistingContactId))
            );

            Assert.Equal("Contact not found", exception.Message);
        }

        public void Dispose()
        {
            _transaction.Rollback();
            _transaction.Dispose();
            _context.Dispose();
        }
    }
}
