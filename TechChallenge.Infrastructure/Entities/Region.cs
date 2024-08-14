namespace TechChallenge.Infrastructure.Entities
{
    public class Region
    {
        public Guid RegionId { get; set; } = Guid.NewGuid();
        public string RegionName { get; set; }
        public IEnumerable<DDD> DDDs { get; set; }
    }
}
