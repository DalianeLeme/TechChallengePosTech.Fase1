using System.Data.Entity;
using TechChallenge.Infrastructure.Entities;

namespace TechChallenge.Infrastructure
{
    public class ContactDbContext : DbContext
    {
        public DbSet<Contact> Contact { get; set; }
    }
}
