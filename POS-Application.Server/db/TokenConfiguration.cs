using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using POS_Application.Server.Models;

namespace POS_Application.Server.db
{
    public class TokenConfiguration : IEntityTypeConfiguration<TokenInfo>
    {
        public void Configure(EntityTypeBuilder<TokenInfo> builder)
        {
            builder.HasKey(u => u.Token);

            builder.Property(a => a.Token)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasIndex(a => a.Token)
                .IsUnique();

            builder.Property(a => a.EmployeeId)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.IsValid)
                .IsRequired();

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(u => u.EmployeeId)
                   .IsRequired();

            //Table & Column Mappings
            builder.ToTable("Tokens");
        }
    }
}
