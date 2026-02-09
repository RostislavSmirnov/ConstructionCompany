using Domain.Entities.Staff;

namespace Application.Abstractions
{
    public interface IAccountRepository
    {
        Task<bool> ExistsByLoginAsync(string login);
        Task<Account> CreateAccountAsync(Account account);
        Task<Account?> GetByLoginAsync(string login);
    }
}
