using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using ShoppingApp.Model;

namespace ShoppingApp.ViewModel
{
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
}
