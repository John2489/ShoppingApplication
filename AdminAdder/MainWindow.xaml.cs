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
using System.IO;
using Microsoft.Win32;


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
            else if (IfImageFileExist(Image.Text))
            {
                DbAdding.AddInDB(Brand.Text, Series.Text, Convert.ToInt32(Cost.Text), Convert.ToInt32(Quantity.Text), GetImageFile(Image.Text));
                MessageBox.Show("Sent successfully!");
                Brand.Text = "";
                Series.Text = "";
                Cost.Text = "";
                Quantity.Text = "";
                Image.Text = @"D:\solutions\acadamy\ShoppingApplication\Data\Images\NONE.jpg";
            }
            else
            {
                MessageBox.Show("File wasn't found.");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"D:\solutions\acadamy\ShoppingApplication\Data\Images\";
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Image.Text = openFileDialog.FileName;
            }
        }
        private static byte[] GetImageFile(string fileName)
        {
            byte[] imageData;
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(fileName, FileMode.Open))
                {
                    imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                }
                return imageData;
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show("File wasn't found.");
                return null;
            }
        }
        private static bool IfImageFileExist(string fileName) => File.Exists(fileName);

    }
}
