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

    public class BillItemLinkedIngredientConfiguration : IEntityTypeConfiguration<BillItemLinkedIngredient>
    {
        public void Configure(EntityTypeBuilder<BillItemLinkedIngredient> builder)
        {
            builder.HasKey(i => i.IngredientId); // Assuming IngredientId is the primary key

            // Additional configurations for properties, if needed
            builder.Property(i => i.Name)
                .HasMaxLength(100) // Example: set maximum length for Name property
                .IsRequired();

            builder.Property(i => i.Quantity)
                .IsRequired();

            builder.Property(i => i.Price)
                .HasColumnType("decimal(10, 2)") // Example type configuration for Price
                .IsRequired();

            builder.HasOne<BillItem>(bi => bi.BillItem) // Assuming Bill is the related entity
                .WithMany(b => b.Ingredients)
                .HasForeignKey(bi => bi.ItemId)
                .IsRequired();

            builder.ToTable("BillItemLinkedIngredients");
        }
    }
}
