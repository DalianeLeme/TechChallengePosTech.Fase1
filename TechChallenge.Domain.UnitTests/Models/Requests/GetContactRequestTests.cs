using TechChallenge.Domain.Models.Requests;

namespace TechChallenge.Domain.UnitTests.Models.Requests
{
    public class GetContactRequestTests
    {
        [Fact]
        [Trait("Category", "UnitTest")]
        public void GetContactRequest_ConstructorWithNullInt()
        {
            //Arrange
            int? ddd = null;

            //Act
            var subject = new GetContactRequest(ddd);

            //Assert
            Assert.Null(subject.DDD);
            Assert.Equal(ddd, subject.DDD);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        public void GetContactRequest_ConstructorWithValueInt()
        {
            //Arrange
            int? ddd = 21;

            //Act
            var subject = new GetContactRequest(ddd);

            //Assert
            Assert.NotNull(subject.DDD);
            Assert.Equal(ddd, subject.DDD);
        }
    }
}
