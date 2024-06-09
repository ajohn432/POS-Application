using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using POS_Application.Server.Models;

namespace POS_Application.Server.db
{
    public class AuthenticationConfiguration : IEntityTypeConfiguration<Authentication>
    {
        public void Configure(EntityTypeBuilder<Authentication> builder)
        {
            builder.HasKey(u => u.username);

            builder.Property(a => a.username)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(a => a.username)
                .IsUnique();

            builder.Property(a => a.pass)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
