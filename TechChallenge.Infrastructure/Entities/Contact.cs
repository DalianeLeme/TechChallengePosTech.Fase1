namespace TechChallenge.Infrastructure.Entities
{
    public class Contact
    {
        public Guid ContactId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public DDD Ddd { get; set; }
        public Guid DddId { get; set; }
    }
}
