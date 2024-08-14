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
        public DbSet<DDD> DDDs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Região: Tem vários DDDs e (Vários Contatos?)
            //modelBuilder.Entity<Region>()
            //    .HasMany(c => c.Contacts)
            //    .WithOne(d => d.Region)
            //    .HasForeignKey(p => p.ContactId);
            
            modelBuilder.Entity<Region>()
                .HasMany(d => d.DDDs)
                .WithOne(r => r.RegionId)
                .HasForeignKey(p => p.DDDId);

            // Contatos: Tem 1 DDD
            modelBuilder.Entity<Contact>()
                .HasOne(d => d.DDDId)
                .WithMany(c => c.Contacts)
                .HasForeignKey(d => d.DDDId);

            // DDD:tem vários contatos e uma região
            modelBuilder.Entity<DDD>()
                .HasOne(r => r.RegionId)
                .WithMany(c => c.DDDs)
                .HasForeignKey(r => r.RegionId);
        }
    }
}
