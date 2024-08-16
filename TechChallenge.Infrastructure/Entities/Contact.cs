namespace TechChallenge.Infrastructure.Entities
{
    public class Contact
    {
        public Guid ContactId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DDD Ddd { get; set; }
        public Guid DddId { get; set; }

        public Contact(Guid contactId, string name, string email, string phone, DDD ddd, Guid dddId)
        {
            ContactId = contactId;
            Name = name;
            Email = email;
            Phone = phone;
            Ddd = ddd;
            DddId = dddId;
        }
        public Contact()
        {
            
        }
    }
}
