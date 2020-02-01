using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingApp.Model
{
    public static class CRUDoperators
    {
        public static List<Item> GetAll()
        {
            using (ShopContext db = new ShopContext())
            {
                List<Item> items = db.Items.ToList();
                return items;
            }
        }
    }
}
