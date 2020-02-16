using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ShoppingApp.Model;
using System.Linq;
using Logger;

namespace ShoppingApp.ViewModel
{
    public static class ListViewService
    {
        public static int PossibilityOrder()
        {
            ShoppingLogger.logger.Info("Check posibility to order.", Environment.CurrentManagedThreadId);
            List<ImageViewModel> choosenItem = PlaceForAllItems.StaticAllItems.Where(t => !t.NotOrdered).ToList();
            foreach (var item in choosenItem)
            {
                if (CRUDoperators.GetQuantity(item.Id) - item.QuantityOrdered >= 0)
                {
                    return 1;
                }
                if(CRUDoperators.GetQuantity(item.Id) - item.QuantityOrdered < 0)
                {
                    ShoppingLogger.logger.Error($"In DateBase was critical changes with quantity of item {item.Brand}-{item.Series}", Environment.CurrentManagedThreadId);
                    return -1;
                }
            }
            return 0;
        }
        public static void TakeOffAllReserved(ObservableCollection<ImageViewModel> imageViewModels)
        {
            ShoppingLogger.logger.Info("Taking off all reserved item.", Environment.CurrentManagedThreadId);
            foreach (var item in imageViewModels)
            {
                if (!item.NotOrdered)
                {
                    CRUDoperators.SetOrderedItem(item.Id, -(item.QuantityOrdered));
                }
            }
        }
        public static void SetOrderedItem(int id, int sigh)
        {
            ShoppingLogger.logger.Info("Setting order item.", Environment.CurrentManagedThreadId);
            switch (sigh)
            {
                case 1:
                    CRUDoperators.SetOrderedItem(id, PlaceForAllItems.StaticAllItems.Where(t => t.Id == id).FirstOrDefault().QuantityOrdered);
                    break;
                case -1:
                    CRUDoperators.SetOrderedItem(id, -(PlaceForAllItems.StaticAllItems.Where(t => t.Id == id).FirstOrDefault().QuantityOrdered));
                    break;
                default:
                    break;
            }
        }
        public static int GetPermitedQuantity(int id)
        {
            ShoppingLogger.logger.Info("Getting permitted quantity to order.", Environment.CurrentManagedThreadId);
            return CRUDoperators.GetPermitedQuantity(id);
        }
        public static bool IfPermitedQuantity(int id)
        {
            ShoppingLogger.logger.Info("Check if permited quantity ordering.", Environment.CurrentManagedThreadId);
            if (CRUDoperators.GetPermitedQuantity(id) >= PlaceForAllItems.StaticAllItems.Where(t => t.Id == id).FirstOrDefault().QuantityOrdered) return true;
            else return false;
        }
        public static bool IfAnyOrdered()
        {
            ShoppingLogger.logger.Info("Checkinf if any items ordered.", Environment.CurrentManagedThreadId);
            if (PlaceForAllItems.StaticAllItems.FirstOrDefault(t => !t.NotOrdered) != null)
                return true;
            else return false;
        }
    }
}
