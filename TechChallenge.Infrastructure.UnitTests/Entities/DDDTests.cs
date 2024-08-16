using TechChallenge.Infrastructure.Entities;

namespace TechChallenge.Infrastructure.UnitTests.Entities
{
    public class DDDTests
    {
        [Fact]
        [Trait("Category", "UnitTest")]
        public void DDD_Constructor()
        {
            //Arrange
            var DDDId = Guid.NewGuid();
            var DDDCode = 21;

            var regionName = "RegionName";
            var region = new Region() { RegionName = regionName, RegionId = Guid.NewGuid()};

            var contact = new Contact() { ContactId = Guid.NewGuid(), Email = "teste@email", Name = "name", Phone = "1234578"};
            var contacts =new List<Contact>() { contact };

            //Act
            var subject = new DDD() {DDDId = DDDId, DDDCode = DDDCode, Region = region, RegionId = region.RegionId, Contacts = contacts };

            //Assert
            Assert.Equal(DDDId, subject.DDDId);
            Assert.Equal(DDDCode, subject.DDDCode);
            Assert.Equal(region, subject.Region);
            Assert.Equal(region.RegionId, subject.RegionId);
            Assert.Equal(contacts, subject.Contacts);
        }
    }
}
