using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Logger;
using ShoppingApp.ViewModel;

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
            ShoppingLogger.logger.Debug("Creating window instans.", Environment.CurrentManagedThreadId);
            InitializeComponent();
            DataContext = MainViewModel;
            PlaceForAllItems.StaticAllItems = MainViewModel.AllItems;
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            ShoppingLogger.logger.Info("Click Refresh Button.", Environment.CurrentManagedThreadId);
            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;
            ListViewService.TakeOffAllReserved(PlaceForAllItems.StaticAllItems);
            PlaceForAllItems.StaticAllItems = MainViewModel.AllItems;
            orderedQuantLabel.Content = 0;
        }
        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            ShoppingLogger.logger.Info("Click Checkout Button.", Environment.CurrentManagedThreadId);
            switch (ListViewService.PossibilityOrder())
            {
                case 1:
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
                    firstNameTextBox.Visibility = Visibility.Visible;
                    lastNameTextBox.Visibility = Visibility.Visible;
                    emailTextBox.Visibility = Visibility.Visible;
                    contactPhoteTextBox.Visibility = Visibility.Visible;
                    deliveryAddressTextBox.Visibility = Visibility.Visible;
                    deliveryInfoBlock.Visibility = Visibility.Visible;
                    confirmBlockButton.Visibility = Visibility.Visible;
                    backButton.Visibility = Visibility.Visible;

                    CreateOrderedList = new CreateOrderedList(MainViewModel.AllItems);
                    if (PlaceForAllItems.StaticOrder == null)
                    {
                        order = new Order(CreateOrderedList.ChoosenList);
                        this.DataContext = order;
                    }
                    else
                    {
                        this.DataContext = PlaceForAllItems.StaticOrder;
                    }
                    break;
                case 0:
                    if (!ListViewService.IfAnyOrdered())
                    {
                        MessageBox.Show("Sorry, you did ordered nothing.");
                    }
                    break;
                case -1:
                    MessageBox.Show("Sorry, something went wrong, please try again");
                    ListViewService.TakeOffAllReserved(MainViewModel.AllItems);
                    MainViewModel = new MainViewModel();
                    PlaceForAllItems.StaticAllItems = MainViewModel.AllItems;
                    DataContext = MainViewModel;
                    orderedQuantLabel.Content = 0;
                    break;
            }
        }
        private void AddInBasket_Click(object sender, RoutedEventArgs e)
        {
            ShoppingLogger.logger.Info("Click AddInBasket Button.", Environment.CurrentManagedThreadId);
            Button add = (sender as Button);
            if (add.Content.ToString() == "Remove")
            {
                ListViewService.SetOrderedItem((int)add.Tag, -1);
                MainViewModel.AllItems.Where(t => t.Id == (int)add.Tag).FirstOrDefault().NotOrdered = true;
                MainViewModel.AllItems.Where(t => t.Id == (int)add.Tag).FirstOrDefault().Font[1] = "Add";
                MainViewModel.AllItems.Where(t => t.Id == (int)add.Tag).FirstOrDefault().Font[0] = FontWeights.Normal;
                orderedQuantLabel.Content = Convert.ToInt32(orderedQuantLabel.Content) - 1;
            }
            else
            {
                if (ListViewService.IfPermitedQuantity((int)add.Tag))
                {
                    ListViewService.SetOrderedItem((int)add.Tag, 1);
                    orderedQuantLabel.Content = Convert.ToInt32(orderedQuantLabel.Content) + 1;
                    MainViewModel.AllItems.Where(t => t.Id == (int)add.Tag).FirstOrDefault().NotOrdered = false;
                    MainViewModel.AllItems.Where(t => t.Id == (int)add.Tag).FirstOrDefault().Font[1] = "Remove";
                    MainViewModel.AllItems.Where(t => t.Id == (int)add.Tag).FirstOrDefault().Font[0] = FontWeights.UltraBold;
                    add.FontWeight = FontWeights.UltraBold;
                }
                else
                {
                    MessageBox.Show($"Sorry, you can order olny {ListViewService.GetPermitedQuantity((int)add.Tag)} units of this item.");
                }
            }
            mainList.Items.Refresh();
        }
        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            ShoppingLogger.logger.Info("Click Increase Button.", Environment.CurrentManagedThreadId);
            Button increase = (sender as Button);
            if (MainViewModel.AllItems.Where(t => t.Id == (int)increase.Tag).FirstOrDefault().NotOrdered)
            {
                MainViewModel.AllItems.Where(t => t.Id == (int)increase.Tag).FirstOrDefault().QuantityOrdered++;
                mainList.Items.Refresh();
            }
        }
        private void Deacrease_Click(object sender, RoutedEventArgs e)
        {
            ShoppingLogger.logger.Info("Click Deacrease Button.", Environment.CurrentManagedThreadId);
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
            ShoppingLogger.logger.Info("Text in TextBoxOrdered has been changed.", Environment.CurrentManagedThreadId);
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
            ShoppingLogger.logger.Info("Focus has been lost from TextBoxOrbered.", Environment.CurrentManagedThreadId);
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
            ShoppingLogger.logger.Info("Click Back Button.", Environment.CurrentManagedThreadId);
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
            firstNameTextBox.Visibility = Visibility.Hidden;
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
            ShoppingLogger.logger.Info("Click Confirm Button.", Environment.CurrentManagedThreadId);
            if (order.Validation())
            {
                PlaceForAllItems.ifOrderMade = true;
                firstNameTextBox.IsEnabled = false;
                lastNameTextBox.IsEnabled = false;
                emailTextBox.IsEnabled = false;
                contactPhoteTextBox.IsEnabled = false;
                deliveryAddressTextBox.IsEnabled = false;
                backButton.IsEnabled = false;
                confirmButton.IsEnabled = false;
                order.SetId();
                PlaceForAllItems.StaticOrder = order;
                OrderService.SendOrderToDb(order);
                PlaceForAllItems.StaticAllItems = null;
                int s = 0;
                int limit = 20;
                bool result = false;
                string wait = "Please, wait";
                string answer;
                Thread check = new Thread(() =>
                {
                    ShoppingLogger.logger.Debug("Start checking answer from DB by Id.", Environment.CurrentManagedThreadId);
                    while (s != limit)
                    {
                        if (wait == "Please, wait....")
                            wait = "Please, wait";
                        s++;
                        answer = OrderService.CheckAnswer(order.Id);
                        if (answer != null)
                        {
                            ShoppingLogger.logger.Info("Answare came", Environment.CurrentManagedThreadId);
                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                deliveryInfoText.Text = answer;
                            }));
                            result = true;
                            MessageBox.Show($"Your order has been processed successfully. Thanks for trust.");
                            ShoppingLogger.logger.Debug("Return successful answer by Id in DB.", Environment.CurrentManagedThreadId);
                            break;
                        }
                        else
                        {
                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                deliveryInfoText.Text = $"{wait}";
                            }));
                        }
                        Thread.Sleep(1000);
                        wait += ".";
                    }
                    if(!result)
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            deliveryInfoText.Text = $"Thank you, your order has arrived, all information will be sent to you by e-mail.";
                        }));
                    }
                });
                check.IsBackground = true;
                check.Start();
            }
        }
    }
}