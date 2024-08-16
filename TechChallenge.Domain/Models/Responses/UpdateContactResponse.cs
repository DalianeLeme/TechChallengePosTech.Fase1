using TechChallenge.Domain.Models.Base;

namespace TechChallenge.Domain.Models.Responses
{
    public class UpdateContactResponse : BaseResponse
    {
        public UpdateContactResponse(Guid id, string name, string email, int ddd, string phone) : base(id, name, email, ddd, phone)
        {
        }
    }
}
