using TechChallenge.Domain.Models.Base;

namespace TechChallenge.Domain.Models.Requests
{
    public class UpdateContactRequest : BaseRequest
    {
        public Guid Id { get; set; }

        public UpdateContactRequest(Guid id, string name, string email, int ddd, string phone) : base(name, email, ddd, phone)
        {
            Id = id;
        }
    }
}
