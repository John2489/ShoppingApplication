using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;

namespace ShoppingApp.ViewModel
{
    public class CreateOrderedList
    {
        public ObservableCollection<OrderedItem> ChoosenList { get; set; } = new ObservableCollection<OrderedItem>();
        public CreateOrderedList(ObservableCollection<ImageViewModel> imageViewModels)
        {
            foreach (var item in imageViewModels.Where(t => !t.NotOrdered).ToList())
            {
                ChoosenList.Add(new OrderedItem(item.Id, (item.Cost*item.QuantityOrdered), String.Concat(item.Brand, " ", item.Series, " - ordered quantity: ", item.QuantityOrdered, ", position price:", (item.Cost * item.QuantityOrdered).ToString(), "$")));
            }
        }
    }
}
