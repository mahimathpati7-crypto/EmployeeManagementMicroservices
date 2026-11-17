using Microsoft.EntityFrameworkCore;

namespace DepartmentFunctions.Data;

public sealed class DepartmentDbContext : DbContext
{
    public DepartmentDbContext(DbContextOptions<DepartmentDbContext> options)
        : base(options)
    {
    }

    public DbSet<DepartmentRecord> Departments => Set<DepartmentRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DepartmentRecord>(entity =>
        {
            entity.ToTable("Departments");
            entity.HasKey(item => item.DepartmentId);
        });
    }
}

public sealed class DepartmentRecord
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string DepartmentCode { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Location { get; set; }
    public int? ManagerId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
