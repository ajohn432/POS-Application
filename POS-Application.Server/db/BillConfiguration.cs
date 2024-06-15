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

    public class BillLinkedBillItemConfiguration : IEntityTypeConfiguration<LinkedBillItem>
    {
        public void Configure(EntityTypeBuilder<LinkedBillItem> builder)
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

            builder.ToTable("BillLinkedBillItems");
        }
    }

    public class BillLinkedIngredientConfiguration : IEntityTypeConfiguration<LinkedIngredient>
    {
        public void Configure(EntityTypeBuilder<LinkedIngredient> builder)
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

            builder.HasOne<LinkedBillItem>(bi => bi.BillItem) // Assuming Bill is the related entity
                .WithMany(b => b.Ingredients)
                .HasForeignKey(bi => bi.ItemId)
                .IsRequired();

            builder.ToTable("BillLinkedIngredients");
        }
    }

    public class BillLinkedDiscountConfiguration : IEntityTypeConfiguration<LinkedDiscount>
    {
        public void Configure(EntityTypeBuilder<LinkedDiscount> builder)
        {
            builder.HasKey(d => d.DiscountId);

            builder.Property(d => d.DiscountCode)
                .IsRequired();

            builder.Property(d => d.DiscountPercentage)
                .HasColumnType("decimal(5, 2)")
                .IsRequired();

            builder.HasOne<Bill>(bi => bi.Bill) // Assuming Bill is the related entity
                .WithMany(b => b.Discounts)
                .HasForeignKey(bi => bi.BillId)
                .IsRequired();

            builder.ToTable("BillLinkedDiscounts");
        }
    }
}
