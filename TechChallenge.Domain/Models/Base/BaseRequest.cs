namespace TechChallenge.Domain.Models.Base
{
    public class BaseRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int DDD { get; set; }
        public string Phone { get; set; }

        public BaseRequest(string name, string email, int ddd, string phone)
        {
            Name = name;
            Email = email;
            DDD = ddd;
            Phone = phone;
        }
    }
}
