﻿using TechChallenge.Domain.Models.Base;

namespace TechChallenge.Domain.Models.Requests
{
    public class UpdateContactRequest : BaseRequest
    {
        public Guid Id { get; set; }
    }
}
