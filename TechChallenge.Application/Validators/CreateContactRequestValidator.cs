using FluentValidation;

using TechChallenge.Domain.Models.Requests;

namespace TechChallenge.Application.Validators
{
    public class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
    {
        public CreateContactRequestValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage("Nome obrigatório.");
            RuleFor(request => request.Name).MinimumLength(3).WithMessage("O nome deve possuir no minimo 3 caracteres.");
            RuleFor(request => request.Email).EmailAddress().WithMessage("Email inválido.");
            RuleFor(request => request.Email).NotEmpty().WithMessage("Email obrigatório.");
            RuleFor(request => request.DDD).GreaterThan(10).LessThan(100).WithMessage("DDD fora da faixa do Brasil.");
            RuleFor(request => request.Phone).MinimumLength(8).MaximumLength(9).WithMessage("O telefone tem que possuir no minimo 8 números e no máximo 9 números");
            RuleFor(request => request.Phone).NotEmpty().WithMessage("Telefone obrigatório.");
            RuleFor(request => request.Phone).Matches(@"^[0-9]*$").WithMessage("O telefone deve conter apenas números.");
        }
    }
}
