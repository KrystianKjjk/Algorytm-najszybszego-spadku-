using FastestFallProgramExample.Communication;
using FastestFallProgramExample.Events;
using MathFunction;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FastestFallProgramExample.ViewModel
{
    public class CounterLineWindowViewModel : PropertyChangedBase
    {
        private IMessageDialogService _messageDialogService;
        private IEventAggregator _eventAggregator;

        private double _windowWidth;
        private double _windowHeight;
        private double _bitmapSize;

        public CounterLineWindowViewModel(ICounterLineDetailViewModel counterLineDetailViewModel, 
            ICounterLineViewModel counterLineViewModel, 
            IMessageDialogService messageDialogService, 
            IEventAggregator eventAggregator)
        {
            _messageDialogService = messageDialogService;
            _eventAggregator = eventAggregator;

            //CounterLineDetailViewModel = new CounterLineDetailViewModel(_messageDialogService, _eventAggregator);
            //CounterLineViewModel = new CounterLineViewModel(eventAggregator, points, function);
            CounterLineDetailViewModel = counterLineDetailViewModel;
            CounterLineViewModel = counterLineViewModel;

            _eventAggregator.GetEvent<CreateCounterLineEvent>().Subscribe(AfterCrteatingCounterLine);

            CloseCommand = new DelegateCommand(OnClose);
        }

        private void OnClose()
        {
            
        }

        public ICommand CloseCommand { get; }

        public ICounterLineDetailViewModel CounterLineDetailViewModel { get; set; }
        public ICounterLineViewModel CounterLineViewModel { get; set; }
        public double WindowWidth
        {
            get { return _windowWidth; }
            set
            {
                _windowWidth = value;
                OnPropertyChanged();
            }
        }
        public double WindowHeight
        {
            get { return _windowHeight; }
            set
            {
                _windowHeight = value;
                OnPropertyChanged();
            }
        }
        public double BitmapSize
        {
            get { return _bitmapSize; }
            set
            {
                _bitmapSize = value;
                OnPropertyChanged();
            }
        }
        public void Load(IEnumerable<Point> points, string function, double imageSize)
        {
            CalculateWindowHeight(imageSize);
            CalculateWindowWidth(imageSize);
            BitmapSize = imageSize;
        }

        private void AfterCrteatingCounterLine(CreateCounterLineEventArgs obj)
        {
            CalculateWindowHeight(obj.ImegeSize);
            CalculateWindowWidth(obj.ImegeSize);
            BitmapSize = obj.ImegeSize;
            Load(obj.Points,obj.Function, obj.ImegeSize);
        }
        private void CalculateWindowHeight(double size)
        {
            WindowHeight = size + 35;
        }
        private void CalculateWindowWidth(double size)
        {
            WindowWidth = size + 150;
        }
    }
}
