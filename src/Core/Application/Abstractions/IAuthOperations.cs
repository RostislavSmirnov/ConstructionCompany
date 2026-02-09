using Domain.Entities.Staff;

namespace Application.Abstractions
{
    public interface IAuthOperations
    {
        Task<bool> IsLoginExistsAsync(string login);
        Task<(Account Account, Employee Employee)> CreateUserAsync(
            string login,
            string passwordHash,
            string role,
            string name,
            string surname,
            string? patronymic);
    }
}
