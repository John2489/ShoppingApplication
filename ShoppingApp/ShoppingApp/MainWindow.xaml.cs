using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainViewModel;
        }
        private void AddInBasket_Click(object sender, RoutedEventArgs e)
        {
            var id = (sender as Button).Tag.ToString();
            MessageBox.Show(id);
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            //change coursore to "hand" style
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;
        }
        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            //check out
        }
    }
}
