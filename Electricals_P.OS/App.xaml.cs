using System.Windows;

namespace Electricals_PointOfSale
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            LoginWindow login = new LoginWindow();
            MainWindow main = new MainWindow();

            login.ShowDialog();
            main.Show();
        }
    }
}
