namespace TechChallenge.Domain.Models.Base
{
    public class BaseResponse
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public int DDD { get; set; }
        public int Phone { get; set; }

        public BaseResponse(Guid id, string name, string email, int ddd, int phone)
        {
            Id = id;
            Name = name;
            Email = email;
            DDD = ddd;
            Phone = phone;
        }
    }
}
