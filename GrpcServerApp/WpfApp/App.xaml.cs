using GrpcServer;
using System.Windows;

namespace WpfServerApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Server GrpcServer;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            GrpcServer = new Server();

            GrpcServer.Start();

            var window = new MainWindow();
            window.Show();
        }
    }

}
