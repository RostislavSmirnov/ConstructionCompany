using FluentValidation;

namespace Application.Features.SupplyHubFeatures.GetTreeById
{
    public class GetTreeByIdQueryValidator : AbstractValidator<GetTreeByIdQuery>
    {
        public GetTreeByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id узла обязателен");
        }
    }
}
