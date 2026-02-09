using Domain.Entities.Staff;

namespace Application.Abstractions;

public interface IEmployeeRepository
{
    Task<Employee> CreateEmployeeAsync(Employee employee);
}
