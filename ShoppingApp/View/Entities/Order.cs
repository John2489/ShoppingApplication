using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.Text.RegularExpressions;
using System.Linq;

namespace ShoppingApp.ViewModel
{
    public class Order
    {
        public ObservableCollection<OrderedItem> ChoosenList { get; set; }
        public int TotalCost { get; set; }
        public string TotalCostString { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Phone { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public Order(ObservableCollection<OrderedItem> choosenList)
        {
            ChoosenList = choosenList;
            var costs = from t in ChoosenList select t.Cost;
            TotalCost = costs.ToArray().Sum();
            TotalCostString = String.Concat(TotalCost.ToString(), "$");
        }
        public bool Validation()
        {
            string namePattern = @"^[A-Z][a-zA-Z]*$";
            string emailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                             @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            string phoneNumberPattern = @"\d{3,15}?";

            //if ((FirstName.Length < 2) || (FirstName.Length > 15))
            if (!Regex.IsMatch(FirstName, namePattern, RegexOptions.IgnoreCase) || FirstName.Length < 2 || FirstName.Length > 15)
            {
                MessageBox.Show("FirstName is incorrect.");
                return false;
            }
            if (!Regex.IsMatch(LastName, namePattern, RegexOptions.IgnoreCase) || LastName.Length < 2 || LastName.Length > 15)
            {
                MessageBox.Show("LastName is incorrect.");
                return false;
            }
            if (!Regex.IsMatch(Email, emailPattern, RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Incorrect Email address.");
                return false;
            }            
            if (!Regex.IsMatch(Phone, phoneNumberPattern, RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Incorrect Phone number.");
                return false;
            }
            return true;
        }
    }
}
