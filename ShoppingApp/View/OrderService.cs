using System;
using ShoppingApp.Model.ObjectsForDB;
using ShoppingApp.Model;
using System.Linq;

namespace ShoppingApp.ViewModel
{
    public class OrderService
    {
        public static void SendOrderToDb(Order ord)
        {
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
        public static string CheckAnswer(int id) => CRUDoperators.CheckAnswerInDbById(id);
    }
}