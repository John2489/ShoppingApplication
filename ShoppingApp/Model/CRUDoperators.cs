﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoppingApp.Model.DBContexts;
using ShoppingApp.Model.ObjectsForDB;
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
        public static void SendOrder(OrderedObject order)
        {
            using(OrderedContext db = new OrderedContext())
            {
                db.Add(order);
                db.SaveChanges();
            }
        }
        public static OrderedObject GetOrderByID(OrderedObject order)
        {
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
