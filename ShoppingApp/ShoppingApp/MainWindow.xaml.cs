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
        Order order;
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
            orderedQuantLabel.Content = 0;
        }
        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDB.PossibilityOrder())
            {
                //CreateOrderedList = new CreateOrderedList(MainViewModel.AllItems);
                //DataContext = CreateOrderedList;
                mainList.Visibility = Visibility.Hidden;
                chooseProductsLabel.Visibility = Visibility.Hidden;
                quantityOfGoodsOrderedLabel.Visibility = Visibility.Hidden;
                orderedQuantLabel.Visibility = Visibility.Hidden;
                goToCheckOutButton.Visibility = Visibility.Hidden;
                refreshButton.Visibility = Visibility.Hidden;

                checkoutLabel.Visibility = Visibility.Visible;
                totalAmoundLabel.Visibility = Visibility.Visible;
                amountLabel.Visibility = Visibility.Visible;
                resultOrdered.Visibility = Visibility.Visible;
                offerToFillIn.Visibility = Visibility.Visible;
                firstNameLabel.Visibility = Visibility.Visible;
                lastNameLabel.Visibility = Visibility.Visible;
                emailLabel.Visibility = Visibility.Visible;
                contactPhoneLabel.Visibility = Visibility.Visible;
                deliveryAddressLabel.Visibility = Visibility.Visible;
                deliveryInfiLabel.Visibility = Visibility.Visible;
                fistNameTextBox.Visibility = Visibility.Visible;
                lastNameTextBox.Visibility = Visibility.Visible;
                emailTextBox.Visibility = Visibility.Visible;
                contactPhoteTextBox.Visibility = Visibility.Visible;
                deliveryAddressTextBox.Visibility = Visibility.Visible;
                deliveryInfoBlock.Visibility = Visibility.Visible;
                confirmBlockButton.Visibility = Visibility.Visible;
                backButton.Visibility = Visibility.Visible;

                CreateOrderedList = new CreateOrderedList(MainViewModel.AllItems);
                order = new Order(CreateOrderedList.ChoosenList);
                this.DataContext = order;
            }
            else
            {
                CheckDB.TakeOffAllReserved(MainViewModel.AllItems);
                MessageBox.Show("Sorry, something went wrong, please try again");
                MainViewModel = new MainViewModel();
                PlaceForAllItems.StaticAllItems = MainViewModel.AllItems;
                DataContext = MainViewModel;
                orderedQuantLabel.Content = 0;
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
                orderedQuantLabel.Content = Convert.ToInt32(orderedQuantLabel.Content) - 1;
            }
            else
            {
                if (CheckDB.IfPermitedQuantity((int)add.Tag))
                {
                    CheckDB.SetOrderedItem((int)add.Tag, 1);
                    orderedQuantLabel.Content = Convert.ToInt32(orderedQuantLabel.Content) + 1;
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

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            mainList.Visibility = Visibility.Visible;
            chooseProductsLabel.Visibility = Visibility.Visible;
            quantityOfGoodsOrderedLabel.Visibility = Visibility.Visible;
            orderedQuantLabel.Visibility = Visibility.Visible;
            goToCheckOutButton.Visibility = Visibility.Visible;
            refreshButton.Visibility = Visibility.Visible;

            checkoutLabel.Visibility = Visibility.Hidden;
            totalAmoundLabel.Visibility = Visibility.Hidden;
            amountLabel.Visibility = Visibility.Hidden;
            resultOrdered.Visibility = Visibility.Hidden;
            offerToFillIn.Visibility = Visibility.Hidden;
            firstNameLabel.Visibility = Visibility.Hidden;
            lastNameLabel.Visibility = Visibility.Hidden;
            emailLabel.Visibility = Visibility.Hidden;
            contactPhoneLabel.Visibility = Visibility.Hidden;
            deliveryAddressLabel.Visibility = Visibility.Hidden;
            deliveryInfiLabel.Visibility = Visibility.Hidden;
            fistNameTextBox.Visibility = Visibility.Hidden;
            lastNameTextBox.Visibility = Visibility.Hidden;
            emailTextBox.Visibility = Visibility.Hidden;
            contactPhoteTextBox.Visibility = Visibility.Hidden;
            deliveryAddressTextBox.Visibility = Visibility.Hidden;
            deliveryInfoBlock.Visibility = Visibility.Hidden;
            confirmBlockButton.Visibility = Visibility.Hidden;
            backButton.Visibility = Visibility.Hidden;
            DataContext = MainViewModel;
            mainList.Items.Refresh();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (order.Validation())
            {
                MessageBox.Show(order.FirstName);
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
