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
        public static void SetOrderedItem(int id, int reservedQuantity)
        {
            using (ShopContext db = new ShopContext())
            {
                Item item = db.Items.Where(t => t.Id == id).First();
                if(item != null)
                {
                    item.ReservedQuantity += reservedQuantity;
                    db.Items.Update(item);
                }
                db.SaveChanges();
            }
        }

        public static int GetPermitedQuantity(int id)
        {
            int result = 0;
            using (ShopContext db = new ShopContext())
            {
                //проверить на искл если нет в базе
                Item item = db.Items.Where(t => t.Id == id).First();
                if (item != null)
                {
                    result = item.Quantity - item.ReservedQuantity;
                }
                db.SaveChanges();
            }
            return result;
        }
        public static int GetQuantity(int id)
        {
            int result = 0;
            using (ShopContext db = new ShopContext())
            {
                //проверить на искл если нет в базе
                Item item = db.Items.Where(t => t.Id == id).First();
                if (item != null)
                {
                    result = item.Quantity;
                }
            }
            return result;
        }
    }
}
