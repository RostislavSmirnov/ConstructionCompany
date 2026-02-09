using FluentValidation;

namespace Application.Features.AuthFeatures.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Логин обязателен")
                .MaximumLength(100).WithMessage("Логин слишком длинный (максимум 100 символов)");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль обязателен");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Роль обязательна");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя обязательно");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Фамилия обязательна");

            RuleFor(x => x.Patronymic)
                .MaximumLength(100).WithMessage("Отчество слишком длинное")
                .When(x => !string.IsNullOrEmpty(x.Patronymic));
        }
    }
}
