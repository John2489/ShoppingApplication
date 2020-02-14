using System.Windows;
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
            if(!PlaceForAllItems.ifOrderMade)
                ListViewService.TakeOffAllReserved(PlaceForAllItems.StaticAllItems);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MessageBox.Show("Thank you for your trust in our company.\nWe save your time.");
        }
    }
}
