using System;
using ShoppingApp.Model.ObjectsForDB;
using ShoppingApp.Model;
using System.Linq;
using Logger;

namespace ShoppingApp.ViewModel
{
    public class OrderService
    {
        public static void SendOrderToDb(Order ord)
        {
            ShoppingLogger.logger.Info("Sending order to DB.", Environment.CurrentManagedThreadId);
            CRUDoperators.SendOrder(new OrderedObject
            {
                Id = ord.Id,
                FirstName = ord.FirstName,
                LastName = ord.LastName,
                Email = ord.Email,
                Phone = ord.Phone,
                Address = ord.Address,
                Order = String.Concat(ord.ChoosenList.Select(t => t.InfoLine).ToArray<string>()),
                DeliveryInfo = null
            }); ;
        }
        public static string CheckAnswer(int id)
        {
            return CRUDoperators.CheckAnswerInDbById(id);
        }
    }
}