using FluentValidation;

namespace Application.Features.SupplyHubFeatures.DeleteSupplyHub
{
    public class DeleteSupplyHubCommandValidator : AbstractValidator<DeleteSupplyHubCommand>
    {
        public DeleteSupplyHubCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id узла обязателен");
        }
    }
}
