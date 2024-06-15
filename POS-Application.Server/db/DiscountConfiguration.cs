using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS_Application.Server.Models;

namespace POS_Application.Server.db
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(d => d.DiscountCode);

            builder.Property(d => d.DiscountPercentage)
                .HasColumnType("decimal(5, 2)")
                .IsRequired();

            builder.HasOne<Bill>(bi => bi.Bill) // Assuming Bill is the related entity
                .WithMany(b => b.Discounts)
                .HasForeignKey(bi => bi.BillId)
                .IsRequired();
        }
    }
}
