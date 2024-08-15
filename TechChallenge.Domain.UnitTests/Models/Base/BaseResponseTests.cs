using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Domain.Models.Base;

namespace TechChallenge.Domain.UnitTests.Models.Base
{
    public class BaseResponseTests
    {
        [Fact]
        [Trait("Category", "UnitTest")]
        public void BaseResponse_Constructor()
        {
            //Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var email = "test@mail.com";
            var ddd = 21;
            var phone = 999999999;

            //Act
            var subject = new BaseResponse(id, name, email, ddd, phone);

            //Assert
            Assert.Equal(id, subject.Id);
            Assert.Equal(name, subject.Name);
            Assert.Equal(email, subject.Email);
            Assert.Equal(ddd, subject.DDD);
            Assert.Equal(phone, subject.Phone);
        }
    }
}
