using Microsoft.EntityFrameworkCore;
using Employee.Models;

namespace Employee.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Employees table
            modelBuilder.Entity<Models.Employee>()
                .HasKey(e => e.EmployeeId);

            modelBuilder.Entity<Models.Employee>()
                .Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Models.Employee>()
                .Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Models.Employee>()
                .Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Models.Employee>()
                .Property(e => e.PhoneNumber)
                .HasMaxLength(15);

            modelBuilder.Entity<Models.Employee>()
                .Property(e => e.Salary)
                .HasPrecision(18, 2);
        }
    }
}
