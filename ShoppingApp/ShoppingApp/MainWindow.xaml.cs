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

namespace ShoppingApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel MainViewModel { get; set; } = new MainViewModel();
        private CreateOrderedList CreateOrderedList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainViewModel;
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            //change coursore to "hand" style
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;
            orderedQuant.Content = 0;
        }
        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDB.PossibilityOrder(MainViewModel.AllItems))
            {
                CreateOrderedList = new CreateOrderedList(MainViewModel.AllItems);
                DataContext = CreateOrderedList;
                mainList.Visibility = Visibility.Hidden;
                resultOrdered.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Sorry, something went wrong, please try again");
                MainViewModel = new MainViewModel();
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
                MainViewModel.AllItems.Where(t => t.Id.ToString() == add.Tag.ToString()).FirstOrDefault().Ordered = false;
                add.Content = "Add";
                MainViewModel.AllItems.Where(t => t.Id.ToString() == add.Tag.ToString()).FirstOrDefault().Font[1] = "Add";
                MainViewModel.AllItems.Where(t => t.Id.ToString() == add.Tag.ToString()).FirstOrDefault().Font[0] = FontWeights.Normal;
                orderedQuant.Content = Convert.ToInt32(orderedQuant.Content) - 1;

            }
            else
            {
                if (MainViewModel.AllItems.Where(t => t.Id.ToString() == add.Tag.ToString()).FirstOrDefault().QuantityOrdered > MainViewModel.AllItems.Where(t => t.Id.ToString() == add.Tag.ToString()).FirstOrDefault().Quantity)
                {
                    MessageBox.Show($"Sorry, you can order olny {MainViewModel.AllItems.Where(t => t.Id.ToString() == add.Tag.ToString()).FirstOrDefault().Quantity} units");
                }
                else
                {
                    orderedQuant.Content = Convert.ToInt32(orderedQuant.Content) + 1;
                    MainViewModel.AllItems.Where(t => t.Id.ToString() == add.Tag.ToString()).FirstOrDefault().Ordered = true;
                    add.Content = "Remove";
                    MainViewModel.AllItems.Where(t => t.Id.ToString() == add.Tag.ToString()).FirstOrDefault().Font[1] = "Remove";
                    MainViewModel.AllItems.Where(t => t.Id.ToString() == add.Tag.ToString()).FirstOrDefault().Font[0] = FontWeights.UltraBold;
                    add.FontWeight = FontWeights.UltraBold;
                }
            }
            mainList.Items.Refresh();
        }
        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.AllItems.Where(t => t.Id.ToString() == (sender as Button).Tag.ToString()).FirstOrDefault().QuantityOrdered++;
            mainList.Items.Refresh();
        }
        private void Deacrease_Click(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.AllItems.Where(t => t.Id.ToString() == (sender as Button).Tag.ToString()).FirstOrDefault().QuantityOrdered > 10)
            {
                MainViewModel.AllItems.Where(t => t.Id.ToString() == (sender as Button).Tag.ToString()).FirstOrDefault().QuantityOrdered--;
            }
            mainList.Items.Refresh();
        }
        private void TextBoxOrdered_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (int.TryParse(textBox.Text, out _))
            {
                if (Convert.ToInt32(textBox.Text) < 10)
                {
                    MainViewModel.AllItems.Where(t => t.Id.ToString() == (sender as TextBox).Tag.ToString()).FirstOrDefault().QuantityOrdered = 10;
                }
                MainViewModel.AllItems.Where(t => t.Id.ToString() == (sender as TextBox).Tag.ToString()).FirstOrDefault().QuantityOrdered = Convert.ToInt32(textBox.Text);
            }
        }
        private void TextBoxOrdered_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (int.TryParse(textBox.Text, out _))
            {
                if (Convert.ToInt32(textBox.Text) < 10)
                    MainViewModel.AllItems.Where(t => t.Id.ToString() == (sender as TextBox).Tag.ToString()).FirstOrDefault().QuantityOrdered = 10;
                mainList.Items.Refresh();
            }
            else
            {
                MainViewModel.AllItems.Where(t => t.Id.ToString() == (sender as TextBox).Tag.ToString()).FirstOrDefault().QuantityOrdered = 10;
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
