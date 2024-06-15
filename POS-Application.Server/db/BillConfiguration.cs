using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS_Application.Server.Models;

namespace POS_Application.Server.db
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(b => b.BillId);
            builder.Property(a => a.BillId)
                .HasMaxLength(10)
                .IsRequired();
            builder.HasIndex(b => b.BillId)
                .IsUnique();

            builder.Property(b => b.CustomerName)
                .IsRequired();

            builder.HasMany(b => b.Items)
                .WithOne()
                .HasForeignKey(b => b.BillId);

            builder.Property(b => b.Status)
                .IsRequired();

            builder.HasMany(b => b.Discounts)
                .WithOne()
                .HasForeignKey(b => b.BillId);

            builder.Property(b => b.TipAmount)
                .IsRequired();
        }
    }
}
