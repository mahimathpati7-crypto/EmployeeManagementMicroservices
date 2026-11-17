using DepartmentEntity = Department.Models.Department;
using Microsoft.EntityFrameworkCore;

namespace Department.Data;

public class DepartmentDbContext : DbContext
{
    public DepartmentDbContext(DbContextOptions<DepartmentDbContext> options)
        : base(options)
    {
    }

    public DbSet<DepartmentEntity> Departments => Set<DepartmentEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DepartmentEntity>(entity =>
        {
            entity.HasKey(item => item.DepartmentId);
            entity.Property(item => item.IsActive).HasDefaultValue(true);
            entity.Property(item => item.CreatedDate).HasDefaultValueSql("GETDATE()");
        });
    }
}
