using FluentValidation;

namespace Application.Features.BuildingObjectFeatures.CreateBuildingObject
{
    public class CreateBuildingObjectCommandValidator : AbstractValidator<CreateBuildingObjectCommand>
    {
        public CreateBuildingObjectCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название объекта обязательно")
                .MaximumLength(200).WithMessage("Название не должно превышать 200 символов");

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Описание не должно превышать 2000 символов")
                .When(x => x.Description != null);
        }
    }
}
