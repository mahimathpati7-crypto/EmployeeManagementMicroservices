using Microsoft.AspNetCore.Mvc;
using Employee.Repositories;
using Employee.Models;

namespace Employee.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Employee>>> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployeesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving employees");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get employee by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Employee>> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (employee == null)
                    return NotFound($"Employee with ID {id} not found");

                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving employee {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Create a new employee
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Models.Employee>> CreateEmployee([FromBody] Models.Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (await _employeeRepository.EmailExistsAsync(employee.Email))
                {
                    return Conflict(new
                    {
                        message = $"An employee with email '{employee.Email}' already exists."
                    });
                }

                var createdEmployee = await _employeeRepository.CreateEmployeeAsync(employee);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.EmployeeId }, createdEmployee);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error creating employee");
                return Conflict(new { message = "An employee with this email already exists." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating employee");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Update an existing employee
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Models.Employee>> UpdateEmployee(int id, [FromBody] Models.Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (await _employeeRepository.EmailExistsAsync(employee.Email, id))
                {
                    return Conflict(new
                    {
                        message = $"An employee with email '{employee.Email}' already exists."
                    });
                }

                var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(id, employee);
                if (updatedEmployee == null)
                    return NotFound($"Employee with ID {id} not found");

                return Ok(updatedEmployee);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error updating employee {id}", id);
                return Conflict(new { message = "An employee with this email already exists." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating employee {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Delete an employee
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var result = await _employeeRepository.DeleteEmployeeAsync(id);
                if (!result)
                    return NotFound($"Employee with ID {id} not found");

                return Ok(new { message = "Employee deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting employee {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
