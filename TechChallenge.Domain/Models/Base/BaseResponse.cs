namespace TechChallenge.Domain.Models.Base
{
    public class BaseResponse
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public DDDModel DDD { get; set; }
        public int Phone { get; set; }

        public BaseResponse()
        {
            
        }

        public BaseResponse(Guid id, string name, string email, DDDModel ddd, int phone)
        {
            Id = id;
            Name = name;
            Email = email;
            DDD = ddd;
            Phone = phone;
        }
    }
}
