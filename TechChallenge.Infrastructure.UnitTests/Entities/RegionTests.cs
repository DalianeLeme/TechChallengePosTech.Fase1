using TechChallenge.Infrastructure.Entities;

namespace TechChallenge.Infrastructure.UnitTests.Entities
{
    public class RegionTests
    {
        [Fact]
        [Trait("Category", "UnitTest")]
        public void Region_Constructor()
        {
            //Arrange
            var regionId = Guid.NewGuid();
            var regionName = "RegionName";
            var ddd = new DDD() { Contacts = new List<Contact>(), DDDCode = 21, DDDId = Guid.NewGuid(), RegionId = regionId};
            var ddds = new List<DDD>() { ddd };

            //Act
            var subject = new Region() { RegionName = regionName, RegionId = regionId, DDDs = ddds};

            //Assert
            Assert.Equal(regionId, subject.RegionId);
            Assert.Equal(regionName, subject.RegionName);
            Assert.Equal(ddds, subject.DDDs);
        }
    }
}
