using System.Collections.Generic;
using System.Collections.ObjectModel;
using ShoppingApp.Model;
using System.Windows;

namespace ShoppingApp.ViewModel
{
    public class ImageViewModel
    {
        public ImageViewModel(int id, string brand, string series, string image, int cost, int quantiry)
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
        public string Image { get; set; }
        public int Cost { get; set; }
        public int Quantity { get; set; }
        public int QuantityOrdered { get; set; }
        public object[] Font { get; set; } = new object [2] { null, "Add"};
        public bool Ordered { get; set; }
    }

    public class MainViewModel
    {
        public ObservableCollection<ImageViewModel> AllItems { get; set; } = new ObservableCollection<ImageViewModel>();

        public MainViewModel()
        {
            SortComparerBrand sortComparerBrand = new SortComparerBrand();
            List<Item> files = CRUDoperators.GetAll();
            files.Sort(sortComparerBrand);
            foreach (var file in files)
            {
                AllItems.Add(new ImageViewModel(file.Id, file.Name, file.Model, file.Image, file.Cost, file.Quantity));
            }
        }
    }
}
