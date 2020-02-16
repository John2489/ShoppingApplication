using System;
using System.Collections.Generic;
using Logger;
using ShoppingApp.Model.ObjectsForDB;

namespace ShoppingApp.ViewModel
{
    public class SortComparerBrand : IComparer<Item>
    {
        public int Compare(Item i1, Item i2)
        {
            ShoppingLogger.logger.Debug("Comparing items in list.", Environment.CurrentManagedThreadId);
            return i1.Name.CompareTo(i2.Name);
        }
    }
}