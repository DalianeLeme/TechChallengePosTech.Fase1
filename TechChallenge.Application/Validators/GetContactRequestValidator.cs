using FluentValidation;

using TechChallenge.Domain.Models.Requests;

namespace TechChallenge.Application.Validators
{
    public class GetContactRequestValidator : AbstractValidator<GetContactRequest>
    {
        public GetContactRequestValidator()
        {
            RuleFor(request => request.DDD).ExclusiveBetween(10, 100).WithMessage("DDD fora da faixa do Brasil.");
        }
    }
}
