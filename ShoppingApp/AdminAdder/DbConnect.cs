using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace AdminAdder
{
    public partial class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public int Cost { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
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

    static class DbAdding
    {
        public static void AddInDB(string name, string model, int cost, int quantity, string image)
        {
            using (ShopContext db = new ShopContext())
            {
                Item item = new Item
                {
                    Id = DateTime.Now.GetHashCode(),
                    Name = name,
                    Model = model,
                    Cost = cost,
                    Quantity = quantity,
                    Image = image
                };
                db.Items.Add(item);
                db.SaveChanges();
            }
        }
    }
}
