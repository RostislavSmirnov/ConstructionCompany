using FluentValidation;

namespace Application.Features.SupplyHubFeatures.ChangeSupplyHubParent
{
    public class ChangeSupplyHubParentCommandValidator : AbstractValidator<ChangeSupplyHubParentCommand>
    {
        public ChangeSupplyHubParentCommandValidator()
        {
            RuleFor(x => x.SupplyHubId)
                .NotEmpty().WithMessage("Id перемещаемого узла обязателен");

            RuleFor(x => x.NewParentId)
                .Must(id => id == null || id != Guid.Empty)
                .WithMessage("NewParentId, если указан, не должен быть пустым GUID");

            RuleFor(x => x.NewParentId)
                .Must((command, newParentId) => newParentId != command.SupplyHubId)
                .When(x => x.NewParentId.HasValue)
                .WithMessage("Нельзя назначить узел родителем самого себя");
        }
    }
}
