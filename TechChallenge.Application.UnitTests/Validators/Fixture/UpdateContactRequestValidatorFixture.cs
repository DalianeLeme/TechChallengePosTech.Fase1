using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Domain.Models.Requests;

namespace TechChallenge.Application.UnitTests.Validators.Fixture
{
    public class UpdateContactRequestValidatorFixture
    {
        private readonly Guid ID = Guid.NewGuid();
        private const string NAME = "name";
        private const string EMAIL = "test@email.com";
        private const int DDD = 21;
        private const string PHONE = "999999999";

        public UpdateContactRequest Request_Test_Id { get; set; }
        public UpdateContactRequest Request_Test_Name { get; set; }
        public UpdateContactRequest Request_Test_Email { get; set; }
        public UpdateContactRequest Request_Test_DDD { get; set; }
        public UpdateContactRequest Request_Test_Phone { get; set; }

        public UpdateContactRequestValidatorFixture()
        {
            Request_Test_Id = new UpdateContactRequest(ID, NAME, EMAIL, DDD, PHONE);
            Request_Test_Name = new UpdateContactRequest(ID, NAME, EMAIL, DDD, PHONE);
            Request_Test_Email = new UpdateContactRequest(ID, NAME, EMAIL, DDD, PHONE);
            Request_Test_DDD = new UpdateContactRequest(ID, NAME, EMAIL, DDD, PHONE);
            Request_Test_Phone = new UpdateContactRequest(ID, NAME, EMAIL, DDD, PHONE);
        }
    }
}
