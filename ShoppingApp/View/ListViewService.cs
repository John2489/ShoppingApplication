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
        public static bool PossibilityOrder(ObservableCollection<ImageViewModel> imageViewModels)
        {
            List<ImageViewModel> choosenItem = imageViewModels.Where(t => t.Ordered).ToList();
            List<Item> items = CRUDoperators.GetAll();
            foreach (var item in choosenItem)
            {
                if (items.Where(t => t.Id == item.Id).FirstOrDefault() == null) return false;

                Item compar = items.Where(t => t.Id == item.Id).FirstOrDefault();

                if (item.QuantityOrdered < compar.Quantity && item.Cost == compar.Cost)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
    public class OrderedItem
    {
        public OrderedItem(int id, string infoLine)
        {
            Id = id;
            InfoLine = infoLine;
        }
        public int Id { get; set; }
        public string InfoLine { get; set; }
    }

    public class CreateOrderedList
    {
        public ObservableCollection<OrderedItem> ChoosenList { get; set; } = new ObservableCollection<OrderedItem>();
        public CreateOrderedList(ObservableCollection<ImageViewModel> imageViewModels)
        {
            foreach (var item in imageViewModels.Where(t => t.Ordered == true).ToList())
            {
                ChoosenList.Add(new OrderedItem(item.Id, String.Concat(item.Brand, " ", item.Series, " - ordered quantity: ", item.QuantityOrdered, ", position price:", (item.Cost * item.QuantityOrdered).ToString(), "$")));
            } 
        }
    }
}
