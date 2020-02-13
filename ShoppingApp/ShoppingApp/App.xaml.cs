using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using ShoppingApp.ViewModel;

namespace ShoppingApp.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            ListViewService.TakeOffAllReserved(PlaceForAllItems.StaticAllItems);
            MessageBox.Show("Bye!!!");
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MessageBox.Show("Hello!!!");
        }
    }
}
