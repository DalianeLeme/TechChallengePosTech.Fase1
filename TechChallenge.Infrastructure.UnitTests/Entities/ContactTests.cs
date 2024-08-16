using TechChallenge.Infrastructure.Entities;

namespace TechChallenge.Infrastructure.UnitTests.Entities
{
    public class ContactTests
    {
        [Fact]
        [Trait("Category", "UnitTest")]
        public void Contact_Constructor()
        {
            //Arrange
            var contactId = Guid.NewGuid();
            var name = "Name";
            var email = "teste@email";
            var phone = "12345678";

            var regionName = "RegionName";
            var region = new Region() { RegionName = regionName, RegionId = Guid.NewGuid() };

            var DDDId = Guid.NewGuid();
            var DDDCode = 21;

            var DDD = new DDD() { DDDId = DDDId, DDDCode = DDDCode, Region = region, RegionId = region.RegionId };
            var DDDs = new List<DDD>() { DDD };

            //Act
            var subject = new Contact(contactId, name, email, phone, DDD, DDDId);

            //Assert
            Assert.Equal(contactId, subject.ContactId);
            Assert.Equal(name, subject.Name);
            Assert.Equal(email, subject.Email);
            Assert.Equal(phone, subject.Phone);
            Assert.Equal(DDD, subject.Ddd);
            Assert.Equal(DDDId, subject.DddId);
        }
    }
}
