﻿using TechChallenge.Domain.Models.Responses;

namespace TechChallenge.Domain.UnitTests.Models.Responses
{
    public class CreateContactResponseTests
    {
        [Fact]
        [Trait("Category", "UnitTest")]
        public void CreateContactResponse_Constructor()
        {
            //Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var email = "test@mail.com";
            var ddd = 21;
            var phone = "999999999";

            //Act
            var subject = new CreateContactResponse(id, name, email, ddd, phone);

            //Assert
            Assert.Equal(id, subject.Id);
            Assert.Equal(name, subject.Name);
            Assert.Equal(email, subject.Email);
            Assert.Equal(ddd, subject.DDD);
            Assert.Equal(phone, subject.Phone);
        }
    }
}