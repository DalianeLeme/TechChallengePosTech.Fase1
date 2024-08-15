namespace TechChallenge.Domain.Models
{
    public class DDDModel
    {
        public Guid DDDId { get; set; } = Guid.NewGuid();
        public RegionModel Region { get; set; }
        public ICollection<ContactModel> Contacts { get; set; }
    }
}
