using System.Collections.Generic;
using System.Collections.ObjectModel;
using ShoppingApp.Model;
using System.Windows;
using System.Linq;
using System;

namespace ShoppingApp.ViewModel
{
    public static class PlaceForAllItems
    {
        public static ObservableCollection<ImageViewModel> StaticAllItems;
    }

    public class ImageViewModel
    {
        public ImageViewModel(int id, string brand, string series, byte[] image, int cost, int quantiry)
        {
            Id = id;
            Brand = brand;
            Series = series;
            Image = image;
            Cost = cost;
            Quantity = quantiry;
            QuantityOrdered = 10;
        }
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Series { get; set; }
        public byte[] Image { get; set; }
        public int Cost { get; set; }
        public int Quantity { get; set; }
        public int QuantityOrdered { get; set; }
        public object[] Font { get; set; } = new object [2] { null, "Add"};
        public bool NotOrdered { get; set; } = true;
    }

    public class MainViewModel
    {
        public ObservableCollection<ImageViewModel> AllItems { get; set; } = new ObservableCollection<ImageViewModel>();
        public string TotalAmount { get; set; } = "100";
        public MainViewModel()
        {
            SortComparerBrand sortComparerBrand = new SortComparerBrand();
            List<Item> files = CRUDoperators.GetAll();
            files.Sort(sortComparerBrand);
            foreach (var file in files)
            {
                if (file.Quantity - file.ReservedQuantity < 10) continue;
                AllItems.Add(new ImageViewModel(file.Id, file.Name, file.Model, file.Image, file.Cost, file.Quantity));
            }
        }
    }
    public class CreateOrderedList
    {
        public ObservableCollection<OrderedItem> ChoosenList { get; set; } = new ObservableCollection<OrderedItem>();
        public CreateOrderedList(ObservableCollection<ImageViewModel> imageViewModels)
        {
            foreach (var item in imageViewModels.Where(t => !t.NotOrdered).ToList())
            {
                ChoosenList.Add(new OrderedItem(item.Id, String.Concat(item.Brand, " ", item.Series, " - ordered quantity: ", item.QuantityOrdered, ", position price:", (item.Cost * item.QuantityOrdered).ToString(), "$")));
            }
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
}
