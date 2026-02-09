using Domain.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlLite.DbContexts;
using Application.Abstractions;

namespace Services.Security
{
    public class AuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly JwtService _jwtService;
        ConstructionCompanyDbContext _dbContext;

        public AuthService(
            IAccountRepository accountRepository,
            IEmployeeRepository employeeRepository,
            IPasswordHasher passwordHasher,
            JwtService jwtService,
            ConstructionCompanyDbContext dbContext)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _dbContext = dbContext;
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

            var account = Account.Create(login, passwordHash, role);

            var employee = Employee.Create(name, surname, patronymic, account);

            await _accountRepository.CreateAccountAsync(account);
            await _employeeRepository.CreateEmployeeAsync(employee);

            return _jwtService.GenerateToken(account);
        }

        public async Task<string?> LoginAsync(string login, string password)
        {
            Account? account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Login == login);

            if (account is null)
                return null;

            if (!_passwordHasher.Verify(password, account.PasswordHash))
                return null;

            return _jwtService.GenerateToken(account);
        }
    }
}
