using System.Security.Cryptography.X509Certificates;
using TechChallenge.Domain.Models.Base;

namespace TechChallenge.Domain.Models.Responses
{
    public class CreateContactResponse : BaseResponse
    {
        public CreateContactResponse(Guid id, string name, string email, int ddd, int phone) : base(id, name, email, ddd, phone)
        {
        }
    }
}
