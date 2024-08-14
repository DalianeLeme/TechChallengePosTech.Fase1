namespace TechChallenge.Infrastructure.Entities
{
    public class Region
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string DDD { get; set; }
        public string RegionName { get; set; }
    }
}
