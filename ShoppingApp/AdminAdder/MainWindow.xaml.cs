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


namespace AdminAdder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Brand.Text == null || Brand.Text == "")
            {
                MessageBox.Show("Enter Brand!");
            }
            else if (Series.Text == null || Series.Text == "")
            {
                MessageBox.Show("Enter Series!");
            }
            else if (!Validation.IfOnlyNumber(Quantity.Text))
            {
                MessageBox.Show("Fild is free or you entered not a number!");
            }
            else if (!Validation.IfOnlyNumber(Cost.Text))
            {
                MessageBox.Show("Fild is free or you entered not a number!");
            }
            else if (Image.Text == null || Image.Text == "")
            {
                Image.Text = @"D:\solutions\acadamy\ShoppingApplication\Data\Images\NONE.jpg";
            }
            else
            {
                DbAdding.AddInDB(Brand.Text, Series.Text, Convert.ToInt32(Cost.Text), Convert.ToInt32(Quantity.Text), Image.Text);
                MessageBox.Show("Sent successfully!");
                Brand.Text = "";
                Series.Text = "";
                Cost.Text = "";
                Quantity.Text = "";
                Image.Text = @"D:\solutions\acadamy\ShoppingApplication\Data\Images\NONE.jpg";
            }
        }
    }
}
