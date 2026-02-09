using FluentValidation;

namespace Application.Features.SupplyHubFeatures.CreateSupplyHub
{
    public class CreateSupplyHubCommandValidator : AbstractValidator<CreateSupplyHubCommand>
    {
        public CreateSupplyHubCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название узла обязательно")
                .MaximumLength(200).WithMessage("Название не должно превышать 200 символов");

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Описание не должно превышать 2000 символов")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.BuildingObjectId)
                .NotEmpty().WithMessage("BuildingObjectId обязателен");

            RuleFor(x => x.ParentId)
                .Must(id => id == null || id != Guid.Empty)
                .WithMessage("ParentId, если указан, не должен быть пустым GUID");
        }
    }
}
