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
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.ContactId);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Phone).IsRequired();

                entity.HasOne(d => d.Ddd)
                .WithMany(c => c.Contacts)
                .HasForeignKey(d => d.DddId);
            });

            modelBuilder.Entity<DDD>(entity =>
            {
                entity.HasKey(e => e.DDDId);

                entity.HasOne(r => r.Region)
                .WithMany(c => c.DDDs)
                .HasForeignKey(r => r.RegionId);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.RegionId);
                entity.Property(e => e.RegionName).IsRequired();
            });
        }
    }
}
