using Flunt.Notifications;
using Flunt.Validations;
using System.Text.RegularExpressions;
using TechChallenge.Domain.Models.Requests;

namespace TechChallenge.Domain.Models.Base
{
    public class BaseRequest : Notifiable<Notification>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int DDD { get; set; }
        public int Phone { get; set; }

        public BaseRequest(string name, string email, int ddd, int phone)
        {
            Name = name;
            Email = email;
            DDD = ddd;
            Phone = phone;
        }
        public void Validate()
        {
            AddNotifications(
                 new Contract<BaseRequest>()
                    .Requires()
                    .IsGreaterThan(Name, 2, "Name", "Nome deve ter no minimo 3 caracteres.")
                    .IsEmail(Email, "Email", "E-mail inválido.")
                    .IsGreaterThan(Phone, 7, "Phone", "Telefone tem que ter no minimo 8 números.")
                    .IsLowerThan(Phone, 10, "Phone", "Telefone tem que ter no máximo 9 números.")
                    .IsGreaterThan(DDD, 10, "DDD está abaixo da faixa no Brasil.")
                    .IsLowerThan(DDD, 100, "DDD está acima da faixa no Brasil.")
            );

            if (!Regex.IsMatch(Phone.ToString(), @"^\d+$"))
                AddNotification("Phone", "Telefone aceita apenas números");
        }
    }
}
