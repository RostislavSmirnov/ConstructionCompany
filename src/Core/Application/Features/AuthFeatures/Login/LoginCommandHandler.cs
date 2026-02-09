using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities.Staff;
using MediatR;
using Services.Security;

namespace Application.Features.AuthFeatures.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string?>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly JwtService _jwtService;
        public LoginCommandHandler(IAccountRepository accountRepository, IPasswordHasher passwordHasher, JwtService jwtService)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<string?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            Account? account = await _accountRepository.GetByLoginAsync(request.Login);

            if (account is null)
                return null;

            if (!_passwordHasher.Verify(request.Password, account.PasswordHash))
                return null;

            return _jwtService.GenerateToken(account);
        }
    }
}
