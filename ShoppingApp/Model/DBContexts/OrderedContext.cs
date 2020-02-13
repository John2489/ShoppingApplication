using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Model.ObjectsForDB;

namespace ShoppingApp.Model.DBContexts
{
    public partial class OrderedContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-0455NAR\SQLEXPRESS;Database=orders;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public virtual DbSet<OrderedObject> Orders { get; set; }
    }
}
