using Autofac;
using FastestFallProgramExample.Communication;
using FastestFallProgramExample.Startup;
using FastestFallProgramExample.View;
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

            var bootstaper = new Bootstrapper();

            var container = bootstaper.Bootsrap();
            var mainWindow = container.Resolve<MainWindow>();
            var counterLineWindow = container.Resolve<CounterLineWindow>();
            //var counterLineWindow = container.Resolve<CounterLineWindow>();
            //var mwm = container.Resolve<MainViewModel>();
            //   mwm.ShowCounterLineWindowEvent += () => counterLineWindow.Show();\

            
            mainWindow.showCLWEvent += () =>
            {
                counterLineWindow.Show();
                //counterLineWindow.CloseCLWEvent += () => counterLineWindow = container.Resolve<CounterLineWindow>();
            };
            mainWindow.Show();

            

            //counterLineWindow.
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
