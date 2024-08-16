using TechChallenge.Domain.Models.Base;
using TechChallenge.Domain.Models.Responses;

namespace TechChallenge.Domain.UnitTests.Models.Responses
{
    public class GetContactResponseTests
    {
        #region Contacts

        #region Contact_1
        private static readonly Guid id_contact_1 = Guid.NewGuid();
        private static readonly string name_contact_1 = "name_1";
        private static readonly string email_contact_1 = "contact_1@email.com";
        private static readonly int ddd_contact_1 = 21;
        private static readonly string phone_contact_1 = "999999999";

        private static BaseResponse Contact_1 = new BaseResponse(id_contact_1, name_contact_1, email_contact_1, ddd_contact_1, phone_contact_1);
        #endregion Contact_1

        #region Contact_2
        private static readonly Guid id_contact_2 = Guid.NewGuid();
        private static readonly string name_contact_2 = "name_2";
        private static readonly string email_contact_2 = "contact_2@email.com";
        private static readonly int ddd_contact_2 = 11;
        private static readonly string phone_contact_2 = "999999998";

        private static BaseResponse Contact_2 = new BaseResponse(id_contact_2, name_contact_2, email_contact_2, ddd_contact_2, phone_contact_2);
        #endregion Contact_2

        #region Contact_3
        private static readonly Guid id_contact_3 = Guid.NewGuid();
        private static readonly string name_contact_3 = "name_3";
        private static readonly string email_contact_3 = "contact_3@email.com";
        private static readonly int ddd_contact_3 = 21;
        private static readonly string phone_contact_3 = "999999997";

        private static BaseResponse Contact_3 = new BaseResponse(id_contact_3, name_contact_3, email_contact_3, ddd_contact_3, phone_contact_3);
        #endregion Contact_3

        private List<BaseResponse> Contacts = new List<BaseResponse> { Contact_1, Contact_2, Contact_3 };
        #endregion Contacts

        [Fact]
        [Trait("Category", "UnitTest")]
        public void GetContactResponse_Constructor()
        {
            //Act
            var subject = new GetContactResponse(Contacts);

            //Assert
            Assert.True(subject.Contacts.Count == 3);
            Assert.Collection(subject.Contacts, new Action<BaseResponse>[]
            {
                (contact) =>
                {
                    Assert.Equal(id_contact_1, contact.Id);
                    Assert.Equal(name_contact_1, contact.Name);
                    Assert.Equal(email_contact_1, contact.Email);
                    Assert.Equal(ddd_contact_1, contact.DDD);
                    Assert.Equal(phone_contact_1, contact.Phone);
                },
                (contact) =>
                {
                    Assert.Equal(id_contact_2, contact.Id);
                    Assert.Equal(name_contact_2, contact.Name);
                    Assert.Equal(email_contact_2, contact.Email);
                    Assert.Equal(ddd_contact_2, contact.DDD);
                    Assert.Equal(phone_contact_2, contact.Phone);
                },
                (contact) =>
                {
                    Assert.Equal(id_contact_3, contact.Id);
                    Assert.Equal(name_contact_3, contact.Name);
                    Assert.Equal(email_contact_3, contact.Email);
                    Assert.Equal(ddd_contact_3, contact.DDD);
                    Assert.Equal(phone_contact_3, contact.Phone);
                },
            });
        }
    }
}
