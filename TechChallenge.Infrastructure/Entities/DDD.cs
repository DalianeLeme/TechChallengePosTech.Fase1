namespace TechChallenge.Infrastructure.Entities
{
    public class DDD
    {
        public Guid DDDId { get; set; } = Guid.NewGuid();
        public Region RegionId { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
    }
}
