using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShoppingApp.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace ShoppingApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private MainViewModel MainViewModel { get; set; } = new MainViewModel();
        private CreateOrderedList CreateOrderedList { get; set; }
        private string TotalAmount { get; set; } = "Hidden";
        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainViewModel;
            PlaceForAllItems.StaticAllItems = MainViewModel.AllItems;
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            //change coursore to "hand" style
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;
            PlaceForAllItems.StaticAllItems = MainViewModel.AllItems;
            orderedQuant.Content = 0;
        }
        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDB.PossibilityOrder())
            {
                CreateOrderedList = new CreateOrderedList(MainViewModel.AllItems);
                DataContext = CreateOrderedList;
                mainList.Visibility = Visibility.Hidden;
                resultOrdered.Visibility = Visibility.Visible;
            }
            else
            {
                CheckDB.TakeOffAllReserved(MainViewModel.AllItems);
                MessageBox.Show("Sorry, something went wrong, please try again");
                MainViewModel = new MainViewModel();
                PlaceForAllItems.StaticAllItems = MainViewModel.AllItems;
                DataContext = MainViewModel;
                orderedQuant.Content = 0;
            }

            /*
            MainWindow2 mainWindow2 = new MainWindow2();
            mainWindow2.Height = 200;
            mainWindow2.Width = 200;
            mainWindow2.Show();
            */
        }
        private void AddInBasket_Click(object sender, RoutedEventArgs e)
        {
            Button add = (sender as Button);
            if (add.Content.ToString() == "Remove")
            {
                CheckDB.SetOrderedItem((int)add.Tag, -1);
                MainViewModel.AllItems.Where(t => t.Id == (int)add.Tag).FirstOrDefault().NotOrdered = true;
                MainViewModel.AllItems.Where(t => t.Id == (int)add.Tag).FirstOrDefault().Font[1] = "Add";
                MainViewModel.AllItems.Where(t => t.Id == (int)add.Tag).FirstOrDefault().Font[0] = FontWeights.Normal;
                orderedQuant.Content = Convert.ToInt32(orderedQuant.Content) - 1;
            }
            else
            {
                if (CheckDB.IfPermitedQuantity((int)add.Tag))
                {
                    CheckDB.SetOrderedItem((int)add.Tag, 1);
                    orderedQuant.Content = Convert.ToInt32(orderedQuant.Content) + 1;
                    MainViewModel.AllItems.Where(t => t.Id == (int)add.Tag).FirstOrDefault().NotOrdered = false;
                    MainViewModel.AllItems.Where(t => t.Id == (int)add.Tag).FirstOrDefault().Font[1] = "Remove";
                    MainViewModel.AllItems.Where(t => t.Id == (int)add.Tag).FirstOrDefault().Font[0] = FontWeights.UltraBold;
                    add.FontWeight = FontWeights.UltraBold;
                }
                else
                {
                    MessageBox.Show($"Sorry, you can order olny {CheckDB.GetPermitedQuantity((int)add.Tag)} units of this item.");
                }
            }
            mainList.Items.Refresh();
        }
        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            Button increase = (sender as Button);
            if (MainViewModel.AllItems.Where(t => t.Id == (int)increase.Tag).FirstOrDefault().NotOrdered)
            {
                MainViewModel.AllItems.Where(t => t.Id == (int)increase.Tag).FirstOrDefault().QuantityOrdered++;
                mainList.Items.Refresh();
            }
        }
        private void Deacrease_Click(object sender, RoutedEventArgs e)
        {
            Button decrease = (sender as Button);
            if (MainViewModel.AllItems.Where(t => t.Id == (int)decrease.Tag).FirstOrDefault().NotOrdered)
            {
                if (MainViewModel.AllItems.Where(t => t.Id == (int)decrease.Tag).FirstOrDefault().QuantityOrdered > 10)
                {
                    MainViewModel.AllItems.Where(t => t.Id == (int)decrease.Tag).FirstOrDefault().QuantityOrdered--;
                }
                mainList.Items.Refresh();
            }
        }
        private void TextBoxOrdered_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (int.TryParse(textBox.Text, out _))
            {
                if (Convert.ToInt32(textBox.Text) < 10)
                {
                    MainViewModel.AllItems.Where(t => t.Id == (int)(sender as TextBox).Tag).FirstOrDefault().QuantityOrdered = 10;
                }
                MainViewModel.AllItems.Where(t => t.Id == (int)(sender as TextBox).Tag).FirstOrDefault().QuantityOrdered = Convert.ToInt32(textBox.Text);
            }
        }
        private void TextBoxOrdered_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (int.TryParse(textBox.Text, out _))
            {
                if (Convert.ToInt32(textBox.Text) < 10)
                    MainViewModel.AllItems.Where(t => t.Id == (int)(sender as TextBox).Tag).FirstOrDefault().QuantityOrdered = 10;
                mainList.Items.Refresh();
            }
            else
            {
                MainViewModel.AllItems.Where(t => t.Id == (int)(sender as TextBox).Tag).FirstOrDefault().QuantityOrdered = 10;
                mainList.Items.Refresh();
            }
        }
    }
    public partial class MainWindow2 : Window
    {
        public MainWindow2()
        {

        }
    }
}
