using FastestFallProgramExample.ViewModel;
using MahApps.Metro.Controls;
using MathFunction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace FastestFallProgramExample.View
{
    /// <summary>
    /// Interaction logic for CounterLineWindow.xaml
    /// </summary>
    public partial class CounterLineWindow : MetroWindow
    {
        public delegate void CloseCLW();
        public event CloseCLW CloseCLWEvent;
        private CounterLineWindowViewModel _CounterLineViewMododel;
        public CounterLineWindow(CounterLineWindowViewModel counterLineViewModel)
        {
            
            InitializeComponent();
            _CounterLineViewMododel = counterLineViewModel;
            DataContext = _CounterLineViewMododel;
        }
        protected override void OnClosed(EventArgs e)
        {

            CloseCLWEvent?.Invoke();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            
        }
    }
}
