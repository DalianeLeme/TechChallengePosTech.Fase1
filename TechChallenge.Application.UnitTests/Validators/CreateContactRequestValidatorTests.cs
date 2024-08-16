﻿using TechChallenge.Application.UnitTests.Validators.Fixture;
using TechChallenge.Application.Validators;

namespace TechChallenge.Application.UnitTests.Validators
{
    public class CreateContactRequestValidatorTests : IClassFixture<CreateContactRequestValidatorFixture>
    {
        private readonly CreateContactRequestValidator _validator;
        private readonly CreateContactRequestValidatorFixture _fixture;

        public CreateContactRequestValidatorTests(CreateContactRequestValidatorFixture fixture)
        {
            _validator = new CreateContactRequestValidator();
            _fixture = fixture;
        }

        [Theory]
        [InlineData(null, "Nome obrigatório.")]
        [InlineData("", "Nome obrigatório.")]
        [InlineData("a", "O nome deve possuir no minimo 3 caracteres.")]
        [InlineData("aa", "O nome deve possuir no minimo 3 caracteres.")]
        [Trait("Category", "UnitTest")]
        public void CreateContactRequest_NameErrors(string name, string expectedMessage)
        {
            //Arrange
            var request = _fixture.Request_Test_Name;
            request.Name = name;

            //Act
            var resultValidations = _validator.Validate(request);

            //Assert
            Assert.Equal(expectedMessage, resultValidations.Errors.Single().ErrorMessage);
        }

        [Theory]
        [InlineData(null, "Email obrigatório.")]
        [InlineData("", "Email obrigatório.")]
        [InlineData("email", "Email inválido.")]
        [InlineData("a", "Email inválido.")]
        [Trait("Category", "UnitTest")]
        public void CreateContactRequest_EmailErrors(string email, string expectedMessage)
        {
            //Arrange
            var request = _fixture.Request_Test_Email;
            request.Email = email;

            //Act
            var resultValidations = _validator.Validate(request);

            //Assert
            Assert.Equal(expectedMessage, resultValidations.Errors.Single().ErrorMessage);
        }

        [Theory]
        [InlineData(null, "DDD fora da faixa do Brasil.")]
        [InlineData(9, "DDD fora da faixa do Brasil.")]
        [InlineData(10, "DDD fora da faixa do Brasil.")]
        [InlineData(100, "DDD fora da faixa do Brasil.")]
        [InlineData(101, "DDD fora da faixa do Brasil.")]
        [Trait("Category", "UnitTest")]
        public void CreateContactRequest_DDDErrors(int ddd, string expectedMessage)
        {
            //Arrange
            var request = _fixture.Request_Test_DDD;
            request.DDD = ddd;

            //Act
            var resultValidations = _validator.Validate(request);

            //Assert
            Assert.Equal(expectedMessage, resultValidations.Errors.Single().ErrorMessage);
        }

        [Theory]
        [InlineData(null, "Telefone obrigatório.")]
        [InlineData("", "Telefone obrigatório.")]
        [InlineData("1234567", "O telefone tem que possuir no mínimo 8 números.")]
        [InlineData("1234567890", "O telefone tem que possuir no máximo 9 números.")]
        [InlineData("99", "O telefone tem que possuir no mínimo 8 números.")]
        [InlineData("aaaaaaaa", "O telefone deve conter apenas números.")]
        [Trait("Category", "UnitTest")]
        public void CreateContactRequest_PhoneErrors(string phone, string expectedMessage)
        {
            //Arrange
            var request = _fixture.Request_Test_Phone;
            request.Phone = phone;

            //Act
            var resultValidations = _validator.Validate(request);

            //Assert
            Assert.Equal(expectedMessage, resultValidations.Errors.Single().ErrorMessage);
        }
    }
}