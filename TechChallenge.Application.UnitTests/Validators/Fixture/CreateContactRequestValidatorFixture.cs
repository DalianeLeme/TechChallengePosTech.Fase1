using Azure.Core;
using TechChallenge.Domain.Models.Requests;

namespace TechChallenge.Application.UnitTests.Validators.Fixture
{
    public class CreateContactRequestValidatorFixture
    { 
        private const string NAME = "name";
        private const string EMAIL = "test@email.com";
        private const int DDD = 21;
        private const string PHONE = "999999999";

        public CreateContactRequest Request_Test_Name { get; set; }
        public CreateContactRequest Request_Test_Email { get; set; }
        public CreateContactRequest Request_Test_DDD { get; set; }
        public CreateContactRequest Request_Test_Phone { get; set; }

        public CreateContactRequestValidatorFixture()
        {
            Request_Test_Name = new CreateContactRequest(NAME, EMAIL, DDD, PHONE);
            Request_Test_Email = new CreateContactRequest(NAME, EMAIL, DDD, PHONE);
            Request_Test_DDD = new CreateContactRequest(NAME, EMAIL, DDD, PHONE);
            Request_Test_Phone = new CreateContactRequest(NAME, EMAIL, DDD, PHONE);
        }
    }
}
