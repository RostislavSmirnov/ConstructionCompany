using Application.Abstractions;
using Domain.Entities.Staff;
using MediatR;
using Services.Security;


namespace Application.Features.AuthFeatures.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly JwtService _jwtService;
        public RegisterCommandHandler(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IPasswordHasher passwordHasher, JwtService jwtService)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }


        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _accountRepository.ExistsByLoginAsync(request.Login))
                throw new InvalidOperationException("Пользователь с таким логином уже существует");

            string passwordHash = _passwordHasher.Hash(request.Password);

            Account account = Account.Create(request.Login, passwordHash, request.Role);

            Employee employee = Employee.Create(
                request.Name,
                request.Surname,
                request.Patronymic,
                account);

            await _accountRepository.CreateAccountAsync(account);
            await _employeeRepository.CreateEmployeeAsync(employee);

            return _jwtService.GenerateToken(account);
        }
    }
}
