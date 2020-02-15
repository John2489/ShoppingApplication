using System.Windows;
using ShoppingApp.ViewModel;
using Logger;
using System;

namespace ShoppingApp.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            ShoppingLogger.logger.Info("Closing application.", Environment.CurrentManagedThreadId);
            if(!PlaceForAllItems.ifOrderMade)
                ListViewService.TakeOffAllReserved(PlaceForAllItems.StaticAllItems);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ShoppingLogger.InitLogger();
            ShoppingLogger.logger.Info("Open application.", Environment.CurrentManagedThreadId);
            MessageBox.Show("Thank you for your trust in our company.\nWe save your time.");
        }
    }
}
