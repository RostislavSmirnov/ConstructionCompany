using FluentValidation;

namespace Application.Features.SupplyHubFeatures.GetSupplyHub
{
    public class GetSupplyHubQueryValidator : AbstractValidator<GetSupplyHubQuery>
    {
        public GetSupplyHubQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id узла обязателен");
        }
    }
}
