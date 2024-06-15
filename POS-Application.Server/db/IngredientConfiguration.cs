using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using POS_Application.Server.Models;

namespace POS_Application.Server.db
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
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
        }
    }
}
