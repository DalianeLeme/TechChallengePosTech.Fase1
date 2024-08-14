using TechChallenge.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace TechChallenge.Application.Services
{
    public class ContactService
    {
        public ContactService()
        {
            
        }

        public async Task<IActionResult> CreateContact()
        {
            var context = new ContactDbContext();

        }
    }
}
