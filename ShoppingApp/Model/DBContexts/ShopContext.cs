using Logger;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Model.ObjectsForDB;
using System;

namespace ShoppingApp.Model.DBContexts
{
    public partial class ShopContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ShoppingLogger.logger.Debug("Configuration DbContext for list.", Environment.CurrentManagedThreadId);
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
