using System.Security.Cryptography.X509Certificates;
using TechChallenge.Domain.Models.Base;

namespace TechChallenge.Domain.Models.Responses
{
    public class CreateContactResponse : BaseResponse
    {
        public Guid Id { get; set; }

        public CreateContactResponse()
        {
        }
    }
}
