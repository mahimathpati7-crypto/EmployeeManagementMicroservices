namespace Employee.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Models.Employee>> GetAllEmployeesAsync();
        Task<Models.Employee?> GetEmployeeByIdAsync(int id);
        Task<bool> EmailExistsAsync(string email, int? employeeId = null);
        Task<Models.Employee> CreateEmployeeAsync(Models.Employee employee);
        Task<Models.Employee?> UpdateEmployeeAsync(int id, Models.Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
