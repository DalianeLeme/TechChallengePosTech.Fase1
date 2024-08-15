using TechChallenge.Domain.Models.Requests;

namespace TechChallenge.Domain.UnitTests.Models.Requests
{
    public class UpdateContactRequestTests
    {
        [Fact]
        [Trait("Category", "UnitTest")]
        public void UpdateContactRequest_Constructor()
        {
            //Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var email = "test@mail.com";
            var ddd = 21;
            var phone = 999999999;

            //Act
            var subject = new UpdateContactRequest(id, name, email, ddd, phone);

            //Assert
            Assert.Equal(id, subject.Id);
            Assert.Equal(name, subject.Name);
            Assert.Equal(email, subject.Email);
            Assert.Equal(ddd, subject.DDD);
            Assert.Equal(phone, subject.Phone);
        }
    }
}
