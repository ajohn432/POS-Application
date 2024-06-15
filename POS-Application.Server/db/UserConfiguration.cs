using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using POS_Application.Server.Models;


namespace POS_Application.Server.db
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.EmployeeId);

            builder.Property(a => a.EmployeeId)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(a => a.EmployeeId)
                .IsUnique();

            builder.Property(a => a.Password)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50);

            //Table & Column Mappings
            builder.ToTable("Users");
            builder.Property(u => u.EmployeeId).HasColumnName("EmployeeId");
            builder.Property(u => u.Password).HasColumnName("Password");
            builder.Property(u => u.Role).HasColumnName("Role");
        }
    }
}
