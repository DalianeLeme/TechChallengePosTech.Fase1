using TechChallenge.Domain.Models.Base;

namespace TechChallenge.Domain.Models.Requests
{
    public class CreateContactRequest : BaseRequest
    {
        public CreateContactRequest(string name, string email, int ddd, int phone) : base(name, email, ddd, phone)
        {
        }
    }
}
