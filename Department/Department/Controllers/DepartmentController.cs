using Department.Data;
using DepartmentEntity = Department.Models.Department;
using DepartmentRequest = Department.Models.DepartmentRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Department.Controllers;

[ApiController]
[Route("api/departments")]
public class DepartmentController : ControllerBase
{
    private readonly DepartmentDbContext _context;

    public DepartmentController(DepartmentDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentEntity>>> GetAll()
    {
        return Ok(await _context.Departments.AsNoTracking().ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DepartmentEntity>> GetById(int id)
    {
        var department = await _context.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.DepartmentId == id);

        return department is null
            ? NotFound($"Department with ID {id} was not found.")
            : Ok(department);
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentEntity>> Create(DepartmentRequest request)
    {
        if (await _context.Departments.AnyAsync(item =>
            item.DepartmentCode == request.DepartmentCode))
        {
            return Conflict("Department code already exists.");
        }

        var department = new DepartmentEntity
        {
            DepartmentName = request.DepartmentName,
            DepartmentCode = request.DepartmentCode,
            Description = request.Description,
            Location = request.Location,
            ManagerId = request.ManagerId,
            IsActive = request.IsActive,
            CreatedDate = DateTime.Now
        };

        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById),
            new { id = department.DepartmentId }, department);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, DepartmentRequest request)
    {
        var existing = await _context.Departments
            .FirstOrDefaultAsync(item => item.DepartmentId == id);

        if (existing is null)
        {
            return NotFound($"Department with ID {id} was not found.");
        }

        if (await _context.Departments.AnyAsync(item =>
            item.DepartmentCode == request.DepartmentCode &&
            item.DepartmentId != id))
        {
            return Conflict("Department code already exists.");
        }

        existing.DepartmentName = request.DepartmentName;
        existing.DepartmentCode = request.DepartmentCode;
        existing.Description = request.Description;
        existing.Location = request.Location;
        existing.ManagerId = request.ManagerId;
        existing.IsActive = request.IsActive;
        existing.UpdatedDate = DateTime.Now;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var department = await _context.Departments
            .FirstOrDefaultAsync(item => item.DepartmentId == id);

        if (department is null)
        {
            return NotFound($"Department with ID {id} was not found.");
        }

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
