using FluentValidation;

namespace Application.Features.SupplyHubFeatures.UpdateSupplyHub
{
    public class UpdateSupplyHubCommandValidator : AbstractValidator<UpdateSupplyHubCommand>
    {
        public UpdateSupplyHubCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id узла обязателен");

            RuleFor(x => x.Name)
                .MaximumLength(200).WithMessage("Название не должно превышать 200 символов")
                .When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Описание не должно превышать 2000 символов")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}
