using System.ComponentModel.DataAnnotations;

namespace Department.Models;

public class DepartmentRequest
{
    [Required, MaxLength(100)]
    public string DepartmentName { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string DepartmentCode { get; set; } = string.Empty;

    [MaxLength(250)]
    public string? Description { get; set; }

    [MaxLength(100)]
    public string? Location { get; set; }

    public int? ManagerId { get; set; }
    public bool IsActive { get; set; } = true;
}
