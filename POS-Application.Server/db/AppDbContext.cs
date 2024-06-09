using Microsoft.EntityFrameworkCore;
using POS_Application.Server.Models;
using System.Reflection;

namespace POS_Application.Server.db
{
    public class AppDbContext : DbContext
    {
        public DbSet<Authentication> authentications {  get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
