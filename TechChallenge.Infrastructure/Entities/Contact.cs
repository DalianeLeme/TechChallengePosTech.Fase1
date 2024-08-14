namespace TechChallenge.Infrastructure.Entities
{
    public class Contact
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public int DDD { get; set; }
        public int Phone { get; set; }
    }
}
