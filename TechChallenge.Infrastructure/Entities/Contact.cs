﻿namespace TechChallenge.Infrastructure.Entities
{
    public class Contact
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string DDD { get; set; }
        public string Phone { get; set; }

    }
}
