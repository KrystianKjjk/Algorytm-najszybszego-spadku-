using FastestFallProgramExample.Communication;
using FastestFallProgramExample.Events;
using MathFunction;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastestFallProgramExample.ViewModel
{
    public class CounterLineWindowViewModel : PropertyChangedBase
    {
        private IMessageDialogService _messageDialogService;
        private IEventAggregator _eventAggregator;

        private double _windowWidth;
        private double _windowHeight;
        private double _bitmapSize;

        public CounterLineWindowViewModel(IMessageDialogService messageDialogService, IEventAggregator eventAggregator, IEnumerable<Point> points, string function, double imageSize )
        {
            _messageDialogService = messageDialogService;
            _eventAggregator = eventAggregator;
            CalculateWindowHeight(imageSize);
            CalculateWindowWidth(imageSize);
            BitmapSize = imageSize;

            CounterLineDetailViewModel = new CounterLineDetailViewModel(_messageDialogService, _eventAggregator);
            CounterLineViewModel = new CounterLineViewModel(eventAggregator, points, function);

            var p = points.ElementAt(0).ListOfVariables.Max(pq => Math.Abs(pq));
            if (p == 0)
                p = 1;
            CounterLineViewModel.Create(imageSize, 1.1 * p, 15);

            _eventAggregator.GetEvent<CreateCounterLineEvent>().Subscribe(AfterCrteatingCounterLine);
        }

        public CounterLineDetailViewModel CounterLineDetailViewModel { get; set; }
        public ICounterLineCreator CounterLineViewModel { get; set; }
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

        private void AfterCrteatingCounterLine(CreateCounterLineEventArgs obj)
        {
            CalculateWindowHeight(obj.ImegeSize);
            CalculateWindowWidth(obj.ImegeSize);
            BitmapSize = obj.ImegeSize;
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
