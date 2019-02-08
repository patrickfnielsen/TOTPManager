using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TOTPManager.Views;

namespace TOTPManager
{
    public partial class App : Application
    {
        private TaskbarIcon tb;

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            tb = (TaskbarIcon)FindResource("MFAIcon");
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            var window = new SettingsWindow(); //ToDo: Refactor
            window.Show();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Shutdown();
        }
    }
}
