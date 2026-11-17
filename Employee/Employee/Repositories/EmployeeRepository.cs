using Microsoft.EntityFrameworkCore;
using Employee.Data;

namespace Employee.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<List<Models.Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Models.Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<bool> EmailExistsAsync(string email, int? employeeId = null)
        {
            return await _context.Employees.AnyAsync(e =>
                e.Email == email &&
                (!employeeId.HasValue || e.EmployeeId != employeeId.Value));
        }

        public async Task<Models.Employee> CreateEmployeeAsync(Models.Employee employee)
        {
            employee.CreatedDate = DateTime.Now;
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Models.Employee?> UpdateEmployeeAsync(int id, Models.Employee employee)
        {
            var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (existingEmployee == null)
                return null;

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.PhoneNumber = employee.PhoneNumber;
            existingEmployee.Gender = employee.Gender;
            existingEmployee.DateOfBirth = employee.DateOfBirth;
            existingEmployee.JoiningDate = employee.JoiningDate;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.DepartmentId = employee.DepartmentId;
            existingEmployee.MemberId = employee.MemberId;
            existingEmployee.IsActive = employee.IsActive;
            existingEmployee.UpdatedDate = DateTime.Now;

            _context.Employees.Update(existingEmployee);
            await _context.SaveChangesAsync();
            return existingEmployee;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null)
                return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
