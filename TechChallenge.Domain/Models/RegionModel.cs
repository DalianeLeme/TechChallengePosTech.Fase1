namespace TechChallenge.Domain.Models
{
    public class RegionModel
    {
        public Guid RegionId { get; set; } = Guid.NewGuid();
        public string RegionName { get; set; }
        public ICollection<DDDModel> DDDs { get; set; }
    }
}
