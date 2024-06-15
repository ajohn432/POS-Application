using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS_Application.Server.Models;

namespace POS_Application.Server.db
{
    public class BillItemConfiguration : IEntityTypeConfiguration<BillItem>
    {
        public void Configure(EntityTypeBuilder<BillItem> builder)
        {
            builder.HasKey(bi => bi.ItemId); // Define primary key

            // Configure the relationship between BillItem and Bill
            builder.HasOne<Bill>(bi => bi.Bill) // Assuming Bill is the related entity
                .WithMany(b => b.Items)
                .HasForeignKey(bi => bi.BillId)
                .IsRequired();

            // Configure the relationship between BillItem and Ingredient
            builder.HasMany(bi => bi.Ingredients)
                .WithOne()
                .HasForeignKey(ing => ing.IngredientId)
                .IsRequired();

            // Additional configurations for properties, if needed
            builder.Property(bi => bi.Quantity)
                .IsRequired();

            builder.Property(bi => bi.BasePrice)
                .HasColumnType("decimal(18, 2)") // Example type configuration
                .IsRequired();
        }
    }
}
