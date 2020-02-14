using Microsoft.EntityFrameworkCore;
using ShoppingApp.Model.ObjectsForDB;

namespace ShoppingApp.Model.DBContexts
{
    public partial class ShopContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-0455NAR\SQLEXPRESS;Database=mainshop;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public virtual DbSet<Item> Items { get; set; }
    }
}
