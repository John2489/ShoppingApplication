using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ShoppingApp.Model;
using System.Linq;

namespace ShoppingApp.ViewModel
{
    public static class CheckDB
    {
        public static bool PossibilityOrder()
        {
            List<ImageViewModel> choosenItem = PlaceForAllItems.StaticAllItems.Where(t => !t.NotOrdered).ToList();
            foreach (var item in choosenItem)
            {
                if (CRUDoperators.GetQuantity(item.Id) - item.QuantityOrdered >= 0)//IfPermitedQuantity(item.Id))
                {
                    return true;
                }
            }
            return false;
        }
        public static void TakeOffAllReserved(ObservableCollection<ImageViewModel> imageViewModels)
        {
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
        public static int GetPermitedQuantity(int id) => CRUDoperators.GetPermitedQuantity(id);
        public static bool IfPermitedQuantity(int id)
        {
            if (CRUDoperators.GetPermitedQuantity(id) >= PlaceForAllItems.StaticAllItems.Where(t => t.Id == id).FirstOrDefault().QuantityOrdered) return true;
            else return false;
        }
    }
}
