using TechChallenge.Domain.Models.Requests;

namespace TechChallenge.Domain.UnitTests.Models.Requests
{
    public class CreateContactRequestTests
    {
        [Fact]
        [Trait("Category", "UnitTest")]
        public void CreateContactRequest_Constructor()
        {
            //Arrange
            var name = "name";
            var email = "test@mail.com";
            var ddd = 21;
            var phone = "999999999";

            //Act
            var subject = new CreateContactRequest(name, email, ddd, phone);

            //Assert
            Assert.Equal(name, subject.Name);
            Assert.Equal(email, subject.Email);
            Assert.Equal(ddd, subject.DDD);
            Assert.Equal(phone, subject.Phone);
        }
    }
}
