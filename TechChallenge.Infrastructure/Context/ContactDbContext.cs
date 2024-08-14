using Microsoft.EntityFrameworkCore;
using TechChallenge.Infrastructure.Entities;

namespace TechChallenge.Infrastructure.Context
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Region> Regions { get; set; }
    }
}
