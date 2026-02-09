using MediatR;

namespace Application.Features.AuthFeatures.Login
{
    public class LoginCommand : IRequest<string?>
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
