using System.Collections.Generic;
using ShoppingApp.Model.ObjectsForDB;

namespace ShoppingApp.ViewModel
{
    public class SortComparerBrand : IComparer<Item>
    {
        public int Compare(Item i1, Item i2)
        {
            return i1.Name.CompareTo(i2.Name);
        }
    }
}