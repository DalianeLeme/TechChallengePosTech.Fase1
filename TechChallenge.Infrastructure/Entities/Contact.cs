﻿namespace TechChallenge.Infrastructure.Entities
{
    public class Contact
    {
        public Guid ContactId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DDD DDDId { get; set; }
    }
}
