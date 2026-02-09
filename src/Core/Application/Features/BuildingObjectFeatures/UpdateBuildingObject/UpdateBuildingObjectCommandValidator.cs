using FluentValidation;

namespace Application.Features.BuildingObjectFeatures.UpdateBuildingObject
{
    public class UpdateBuildingObjectCommandValidator : AbstractValidator<UpdateBuildingObjectCommand>
    {
        public UpdateBuildingObjectCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id объекта обязателен");

            RuleFor(x => x.Name)
                .MaximumLength(200).WithMessage("Название не должно превышать 200 символов")
                .When(x => x.Name != null);

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Описание не должно превышать 2000 символов")
                .When(x => x.Description != null);
        }
    }
}
