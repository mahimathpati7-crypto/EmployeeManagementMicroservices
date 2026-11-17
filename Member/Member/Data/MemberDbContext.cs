using Member.Models;
using Microsoft.EntityFrameworkCore;
using MemberEntity = Member.Models.Member;

namespace Member.Data;

public class MemberDbContext(DbContextOptions<MemberDbContext> options) : DbContext(options)
{
    public DbSet<MemberEntity> Members => Set<MemberEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MemberEntity>(entity =>
        {
            entity.ToTable("Members");
            entity.HasKey(member => member.MemberId);
            entity.Property(member => member.MemberId).UseIdentityColumn(1, 1);
            entity.Property(member => member.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(member => member.LastName).HasMaxLength(100).IsRequired();
            entity.Property(member => member.Email).HasMaxLength(150).IsRequired();
            entity.Property(member => member.PhoneNumber).HasMaxLength(15);
            entity.Property(member => member.Address).HasMaxLength(250);
            entity.Property(member => member.City).HasMaxLength(100);
            entity.Property(member => member.State).HasMaxLength(100);
            entity.Property(member => member.Country).HasMaxLength(100);
            entity.Property(member => member.MembershipType).HasMaxLength(50).IsRequired();
            entity.Property(member => member.IsActive).HasDefaultValue(true);
            entity.Property(member => member.CreatedDate).HasDefaultValueSql("GETDATE()");
        });
    }
}
