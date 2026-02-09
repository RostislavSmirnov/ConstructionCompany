using Application.Abstractions;
using Domain.Entities.Staff;
using Persistence.SqlLite.DbContexts;

namespace Persistence.SqlLite.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ConstructionCompanyDbContext _dbContext;

        public EmployeeRepository(ConstructionCompanyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }
    }
}
