using FastestFallProgramExample.Communication;
using FastestFallProgramExample.ViewModel;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FastestFallProgramExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void Application_Startup(object sedner, StartupEventArgs e)
        {
            var messageDialogService = new MessageDialogService();
            var eventAggregator = new EventAggregator();
            var mainWindow = new MainWindow(new MainViewModel(messageDialogService, eventAggregator));
            MainWindow.Show();
        }
        private void Application_DispatcherUnhandledException(object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexpected error occured."
                + Environment.NewLine + e.Exception.Message, "Unexpected error");

            e.Handled = true;
        }
    }
}
