using FastestFallProgramExample.Communication;
using FastestFallProgramExample.Events;
using Prism.Commands;
using Prism.Events;
using System.Windows.Input;

namespace FastestFallProgramExample.ViewModel
{
    public class CounterLineDetailViewModel : PropertyChangedBase
    {
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private int _step;
        private double _countrLineSize;
        private double _range;
        private double _x1;
        private double _x2;
        public CounterLineDetailViewModel(IMessageDialogService messageDialogService, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            CreateCommand = new DelegateCommand(OnCreateCommand, OnCancreateCommand);

            Step = 10;
            CounterLineSize = 500;
            Range = 10;
            X1 = 0;
            X2 = 0;

        }

        public ICommand CreateCommand { get; }

        public int Step
        {
            get { return _step; }
            set
            {
                _step = value;
                OnPropertyChanged();
                ((DelegateCommand)CreateCommand).RaiseCanExecuteChanged();
            }
        }
        public double CounterLineSize
        {
            get { return _countrLineSize; }
            set
            {
                _countrLineSize = value;
                OnPropertyChanged();
                ((DelegateCommand)CreateCommand).RaiseCanExecuteChanged();
            }
        }
        public double Range
        {
            get { return _range; }
            set
            {
                _range = value;
                OnPropertyChanged();
                ((DelegateCommand)CreateCommand).RaiseCanExecuteChanged();
            }
        }
        public double X1
        {
            get { return _x1; }
            set
            {
                _x1 = value;
                OnPropertyChanged();
                ((DelegateCommand)CreateCommand).RaiseCanExecuteChanged();
            }
        }
        public double X2
        {
            get { return _x2; }
            set
            {
                _x2 = value;
                OnPropertyChanged();
                ((DelegateCommand)CreateCommand).RaiseCanExecuteChanged();
            }
        }

        private void OnCreateCommand()
        {
            _eventAggregator.GetEvent<CreateCounterLineEvent>().Publish(
                new CreateCounterLineEventArgs
                {
                    ImegeSize = this.CounterLineSize,
                    Range = this.Range,
                    Step = this.Step,
                    X = this.X1,
                    Y = this.X2
                });
        }

        private bool OnCancreateCommand()
        {
            return Step != 0
                && Range != 0
                && CounterLineSize != 0;

        }




    }
}
