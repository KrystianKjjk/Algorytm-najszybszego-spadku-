﻿using Autofac;
using FastestFallProgramExample.View;
using FastestFallProgramExample.ViewModel;
using MahApps.Metro.Controls;
using MathFunction;
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

namespace FastestFallProgramExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public delegate void showCLW();
        public event showCLW showCLWEvent;
        private MainViewModel _viewModel;
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            _viewModel = mainViewModel;
            DataContext = _viewModel;

            _viewModel.ShowCounterLineWindowEvent += () => showCLWEvent?.Invoke();

        }
    }
}
