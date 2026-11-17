using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Department.Models;

[Table("Departments")]
public class Department
{
    [Key]
    public int DepartmentId { get; set; }

    //[Required, MaxLength(100)]
    [Required(ErrorMessage = "Department Name is required")]
    [StringLength(100, MinimumLength = 2)]
    public string DepartmentName { get; set; } = string.Empty;

    //[Required, MaxLength(50)]
    [Required(ErrorMessage = "Department Code is required")]
    [StringLength(50)]
    public string DepartmentCode { get; set; } = string.Empty;

    [StringLength(250)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Location { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Manager")]
    public int? ManagerId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
