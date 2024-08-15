namespace TechChallenge.Infrastructure.Entities
{
    public class DDD
    {
        public Guid DDDId { get; set; } = Guid.NewGuid();
        public Guid RegionId {  get; set; }
        public Region Region { get; set; }
        public int DDDCode { get; set; }
        public ICollection<Contact> Contacts { get; set; }

        public DDD()
        {
            
        }
    }
}
