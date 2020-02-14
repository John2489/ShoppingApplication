using System.Collections.ObjectModel;

namespace ShoppingApp.ViewModel
{
    public static class PlaceForAllItems
    {
        public static bool ifOrderMade = false;
        public static ObservableCollection<ImageViewModel> StaticAllItems;
        public static Order StaticOrder;
    }
}
