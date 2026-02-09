using Application.Abstractions;
using Domain.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlLite.DbContexts;

namespace Persistence.SqlLite.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ConstructionCompanyDbContext _dbContext;

        public AccountRepository(ConstructionCompanyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByLoginAsync(string login)
        {
            return await _dbContext.Accounts.AnyAsync(a => a.Login == login);
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
            return account;
        }

        public async Task<Account?> GetByLoginAsync(string login)
        {
            return await _dbContext.Accounts
                .FirstOrDefaultAsync(a => a.Login == login);
        }
    }
}
