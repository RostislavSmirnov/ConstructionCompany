using FluentValidation;

namespace Application.Features.BuildingObjectFeatures.GetBuildingObject
{
    public class GetBuildingObjectQueryValidator : AbstractValidator<GetBuildingObjectQuery>
    {
        public GetBuildingObjectQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id обязателен");
        }
    }
}
