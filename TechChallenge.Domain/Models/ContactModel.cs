namespace TechChallenge.Domain.Models
{
    public class ContactModel
    {
        public Guid ContactId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public DDDModel Ddd { get; set; }
    }
}
