namespace Domain.Entities.Staff;

public sealed class Employee
{
    private Employee() { }
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Surname { get; private set; } = null!;
    public string Patronymic { get; private set; } = null!;

    public Account Account { get; private set; } = null!;
    public Guid AccountId { get; private set; }

    public static Employee Create(
        string name,
        string surname,
        string patronymic,
        Account account)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Имя обязательно", nameof(name));

        if (string.IsNullOrWhiteSpace(surname))
            throw new ArgumentException("Фамилия обязательна", nameof(surname));

        if (string.IsNullOrWhiteSpace(patronymic))
            throw new ArgumentException("Отчество обязательна", nameof(surname));

        if (account is null)
            throw new ArgumentNullException(nameof(account));

        return new Employee
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Surname = surname.Trim(),
            Patronymic = patronymic.Trim(),
            Account = account,
            AccountId = account.Id
        };
    }

    public void UpdatePersonalInfo(string name, string surname, string patronymic)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name.Trim();

        if (!string.IsNullOrWhiteSpace(surname))
            Surname = surname.Trim();

        if (!string.IsNullOrWhiteSpace(patronymic))
            Surname = patronymic.Trim();
    }
}

