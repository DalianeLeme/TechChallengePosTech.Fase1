﻿namespace TechChallenge.Domain.Models
{
    public class CreateContactRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int DDD { get; set; }
        public int Phone { get; set; }

        public CreateContactRequest(string name, string email, int ddd, int phone)
        {
            Name = name;
            Email = email;
            DDD = ddd;
            Phone = phone;
        }
    }
}
