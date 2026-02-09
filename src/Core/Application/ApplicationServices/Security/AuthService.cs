using Domain.Entities.Staff;
using Application.Abstractions;

namespace Services.Security
{
    public class AuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly JwtService _jwtService;

        public AuthService(
            IAccountRepository accountRepository,
            IEmployeeRepository employeeRepository,
            IPasswordHasher passwordHasher,
            JwtService jwtService)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<string> RegisterAsync(
            string login,
            string password,
            string role,
            string name,
            string surname,
            string patronymic)
        {
            if (await _accountRepository.ExistsByLoginAsync(login))
                throw new InvalidOperationException("Пользователь с таким логином уже существует");

            string passwordHash = _passwordHasher.Hash(password);

            Account account = Account.Create(login, passwordHash, role);

            Employee employee = Employee.Create(name, surname, patronymic, account);

            await _accountRepository.CreateAccountAsync(account);
            await _employeeRepository.CreateEmployeeAsync(employee);

            return _jwtService.GenerateToken(account);
        }

        public async Task<string?> LoginAsync(string login, string password)
        {
            Account? account = await _accountRepository.GetByLoginAsync(login);

            if (account is null)
                return null;

            if (!_passwordHasher.Verify(password, account.PasswordHash))
                return null;

            return _jwtService.GenerateToken(account);
        }
    }
}
