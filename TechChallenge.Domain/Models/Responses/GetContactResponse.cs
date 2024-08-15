using TechChallenge.Domain.Models.Base;

namespace TechChallenge.Domain.Models.Responses
{
    public class GetContactResponse : BaseResponse
    {
        public IList<BaseResponse> Contacts { get; set; }
    }
}
