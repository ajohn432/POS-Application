using Microsoft.EntityFrameworkCore;
using POS_Application.Server.Models;
using System.Reflection;

namespace POS_Application.Server.db
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TokenInfo> Tokens { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillItem> BaseBillItems { get; set; }
        public DbSet<LinkedBillItem> BillLinkedBillItems { get; set; }
        public DbSet<LinkedIngredient> BillLinkedIngredients { get; set; }
        public DbSet<LinkedDiscount> BillLinkedDiscounts { get; set; }
        public DbSet<BillItemLinkedIngredient> BillItemLinkedIngredients { get; set; }
        public DbSet<Ingredient> BaseIngredients { get; set; }
        public DbSet<Discount> BaseDiscounts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            //modelBuilder.ApplyConfiguration(new TokenConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
