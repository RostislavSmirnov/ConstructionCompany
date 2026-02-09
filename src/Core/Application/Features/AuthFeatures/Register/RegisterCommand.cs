using MediatR;

namespace Application.Features.AuthFeatures.Register
{
    public class RegisterCommand : IRequest<string>
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = "User";
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
    }
}
