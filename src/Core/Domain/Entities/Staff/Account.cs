namespace Domain.Entities.Staff;

public class Account
{
    private Account() { }
    public Guid Id { get; private set; }
    public string Role { get; private set; } = null!;
    public string Login { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    public static Account Create(string login, string passwordHash, string role)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentException("Логин обязателен");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Хэш пароля обязателен");

        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("Роль обязательна");

        return new Account
        {
            Id = Guid.NewGuid(),
            Login = login.Trim(),
            PasswordHash = passwordHash,
            Role = role.Trim()
        };
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("Новый хэш пароля не может быть пустым");

        PasswordHash = newPasswordHash;
    }

    internal string GetPasswordHash() => PasswordHash;
}
