using System;
using System.Collections.Generic;
using System.Linq;
using Logger;
using ShoppingApp.Model.DBContexts;
using ShoppingApp.Model.ObjectsForDB;

namespace ShoppingApp.Model
{
    public static class CRUDoperators
    {
        public static List<Item> GetAll()
        {
            ShoppingLogger.logger.Debug("Getting all items from DB.", Environment.CurrentManagedThreadId);
            using (ShopContext db = new ShopContext())
            {
                List<Item> items = db.Items.ToList();
                return items;
            }
        }
        public static void SetOrderedItem(int id, int reservedQuantity)
        {
            ShoppingLogger.logger.Debug("Setting ordered items to DB.", Environment.CurrentManagedThreadId);
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
            ShoppingLogger.logger.Debug("Getting permited quantity by Id in DB.", Environment.CurrentManagedThreadId);
            int result = 0;
            using (ShopContext db = new ShopContext())
            {
                Item item = db.Items.Where(t => t.Id == id).FirstOrDefault();
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
            ShoppingLogger.logger.Debug("Getting quantity by Id in DB.", Environment.CurrentManagedThreadId);
            int result = 0;
            using (ShopContext db = new ShopContext())
            {
                Item item = db.Items.Where(t => t.Id == id).FirstOrDefault();
                if (item != null)
                {
                    result = item.Quantity;
                }
            }
            return result;
        }
        public static void SendOrder(OrderedObject order)
        {
            ShoppingLogger.logger.Debug("Sending order to DB.", Environment.CurrentManagedThreadId);
            using (OrderedContext db = new OrderedContext())
            {
                db.Add(order);
                db.SaveChanges();
            }
        }
        public static OrderedObject GetOrderByID(OrderedObject order)
        {
            ShoppingLogger.logger.Debug("Getting order by Id in DB.", Environment.CurrentManagedThreadId);
            using (OrderedContext db = new OrderedContext())
            {
                OrderedObject oo = db.Orders.FirstOrDefault(t => t.Id == order.Id);
                return oo;
            }
        }
        public static string CheckAnswerInDbById(int id)
        {
            using (OrderedContext db = new OrderedContext())
            {
                OrderedObject oo = db.Orders.Where(t => t.Id == id).FirstOrDefault();
                if (oo != null)
                {
                    return oo.DeliveryInfo;
                }
                else return null;
            }
        }
    }
}
