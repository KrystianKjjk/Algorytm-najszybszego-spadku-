using Algorithms;
using FastestFallProgramExample.Communication;
using FastestFallProgramExample.Model;
using MathFunction;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FastestFallProgramExample.View;
using FastestFallProgramExample.Events;

namespace FastestFallProgramExample.ViewModel
{
    
    public class MainViewModel : PropertyChangedBase
    {
        private IMessageDialogService _messageDialogService;
        private IEventAggregator _eventAggregator;
        private FunctionDefinition _functionDefinition;
        private Point _Xo;
        private IEnumerable<Point> _calculatedPoint;
        private InputParameters _inputParameters;
        private DataTable _dataTable;
        private string _calculate;
        private string _functionDefinedByUser;
        private bool _changedFunctionSyntax;
        private bool _isCalculating;
        private double _counterLineSize;

        private IEnumerable<Point> calcucatedPoints
        {
            get { return _calculatedPoint; }
            set
            {
                _calculatedPoint = value;
                ((DelegateCommand)CreateCounterLineCommand)?.RaiseCanExecuteChanged();
            }
        }
        public MainViewModel(IMessageDialogService messageDialogService, IEventAggregator eventAggregator)
        {
            _messageDialogService = messageDialogService;
            _eventAggregator = eventAggregator;

            Calculate = "Oblicz";
            FunctionDefinedByUser = "f(x1, x2) = 2 * (x1) ^ 2 + (x2) ^ 2 - 2 * x1 * x2"; // example
            calcucatedPoints = null;
            CounterLineSize = 500;
            _changedFunctionSyntax = false;
            _isCalculating = false;

            SaveCommand = new DelegateCommand(OnSaveCommand, OnCanSaveCommand);
            CalculateMinnimumCommand = new DelegateCommand(OnCalculateCommand, OnCanCalculateCommand);
            CreateCounterLineCommand = new DelegateCommand(OnCreateCommand, OnCanCreateCommand);

            Variables = new ObservableCollection<Variable>();
            FunctionVariables = new ObservableCollection<FunctionVariables> { new FunctionVariables { Variables = Variables, Result = 5 } };
            InputParameters = new InputParameters();
            _functionDefinition = new FunctionDefinition(FunctionDefinedByUser);
            Xo = new Point();
            DataTable = new DataTable();

            UpdateVariablees();
        }
        public delegate void ShowCounterLineWindowEventHandler();
        public event ShowCounterLineWindowEventHandler ShowCounterLineWindowEvent;
        public ICommand SaveCommand { get; }
        public ICommand CalculateMinnimumCommand { get; set; }
        public ICommand CreateCounterLineCommand { get; set; }

        public ObservableCollection<Variable> Variables { get; set; }
        public ObservableCollection<FunctionVariables> FunctionVariables { get; set; }
        public DataTable DataTable
        {
            get { return _dataTable; }
            set
            {
                _dataTable = value;
                OnPropertyChanged();
            }
        }
        public string Calculate
        {
            get { return _calculate; }
            set
            {
                _calculate = value;
                OnPropertyChanged();
            }
        }      
        public string FunctionDefinedByUser
        {
            get { return _functionDefinedByUser; }
            set
            {
                
                _functionDefinedByUser = value;
                _changedFunctionSyntax = true;
                OnPropertyChanged();
                ((DelegateCommand)SaveCommand)?.RaiseCanExecuteChanged();
            }
        }
        public Point Xo
        {
            get { return _Xo; }
            set
            {
                _Xo = value;
                OnPropertyChanged();
            }
           
        }
        public InputParameters InputParameters
        {
            get { return _inputParameters; }
            set
            {
                _inputParameters = value;
                OnPropertyChanged();
                
            }
        }
        public double CounterLineSize
        {
            get { return _counterLineSize; }
            set
            {
                _counterLineSize = value;
                OnPropertyChanged();
            }
        }

        private void OnSaveCommand()
        {
            var oldfun = _functionDefinition.Function.getFunction(0);
            _functionDefinition.SetNewExpression(FunctionDefinedByUser); // have to check this one 

            var result = _functionDefinition.Function.checkSyntax();

            if (result == false)
            {
                _messageDialogService.ShowInfoDialog("Nie poprawna składnia funkcji");
                _functionDefinition.SetNewExpression(oldfun);
                return;
            }
            _changedFunctionSyntax = false;
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (IsArgumentChanged(oldfun))
                UpdateVariablees();
        }
        private bool OnCanSaveCommand()
        {
            return _changedFunctionSyntax;
        }
        private void OnCreateCommand()
        {
            var p = calcucatedPoints.ElementAt(0).ListOfVariables.Max(pq => Math.Abs(pq));
            if (p == 0)
                p = 1;
            _eventAggregator.GetEvent<CreateCounterLineEvent>().Publish(
                new CreateCounterLineEventArgs
                {
                    Range = 1.1*p,
                    Step = 15,
                    Points = calcucatedPoints,
                    Function = FunctionDefinedByUser,
                    ImegeSize = CounterLineSize
                });
            ShowCounterLineWindowEvent?.Invoke(); // open new window

        }
        private bool OnCanCreateCommand()
        {
            return calcucatedPoints != null
                    && Variables.Count() == 2;

        }
        private void OnCalculateCommand()
        {
            if(_changedFunctionSyntax == true)
            {
                var result = _messageDialogService.showOkCancelDialog("Informacja", "Obliczana funkcja jest inna od zapisanej.\nWciśnij 'Ok' w celu zapisania funkcji i kontynuowania obliczeń lub 'Anuluj' w celu anulowania operacji.\n" +
                    "Uwaga! Jeżeli zmieniono ilość argumentów w funkcji, ich wartość po zapisaniu będzie wynosiła : 0.0");

                if (result == MessageDialogResult.Cancel)
                    return;
                else
                    OnSaveCommand();
            }
            
            Task calculateMinimumTask = new Task(CalculateMinimum);
            calculateMinimumTask.Start();

            calculateMinimumTask.ContinueWith((t) => EndCalculating());
        }
        private bool OnCanCalculateCommand()
        {
            return !_isCalculating;
        }

        private void StartCalculating()
        {
            Calculate = "Trwa obliczanie";
            _isCalculating = true;
            ((DelegateCommand)CalculateMinnimumCommand).RaiseCanExecuteChanged();
        }
        private void EndCalculating()
        {
            Calculate = "Oblicz";
            _isCalculating = false;
            ((DelegateCommand)CalculateMinnimumCommand).RaiseCanExecuteChanged();
        }
        private void CalculateMinimum()
        {
            try
            {
            StartCalculating();

            var value = Variables.Select(v => v.Value).ToArray();
            var x0 = new Point(value);
            var NS = new FastestFallAlgorithm(_functionDefinition, x0, InputParameters.Beta, InputParameters.Tau, InputParameters.LIteration, InputParameters.Epsilon1, InputParameters.Epsilon2, InputParameters.Epsilon3);

            calcucatedPoints = NS.Run();
            var result = calcucatedPoints.Last();

            var hessianResult = Hessian.ChceckHessianDeterminants(_functionDefinition, result, InputParameters.Epsilon1);

            UpdateDataTable(calcucatedPoints);

            if (hessianResult == Hessian.StationaryConditions.Minimum)
                _messageDialogService.ShowInfoDialog($"Znaleziono minimum lokalne w punkcie : {result} \nWartość : { _functionDefinition.GetValue(result)}");
            if (hessianResult != Hessian.StationaryConditions.Minimum)
                _messageDialogService.ShowInfoDialog($"Obliczono resultat, który nie jest minimum lokalnym w punkcie : {result} \nWartość : { _functionDefinition.GetValue(result)}");

            }
            catch (InvalidTauException)
            {
                _messageDialogService.ShowInfoDialog("Nie można obliczyć minimum dla podaego tau. Podaj inny parametr");
            }
            catch (UnfortunateFunctionCaseException fex)
            {
                calcucatedPoints = fex.Points;
                UpdateDataTable(fex.Points);
                var lastPoint = fex.Points.Last();
                _messageDialogService.ShowInfoDialog($"Algorytm zakończył działanie z powodu zbyt dużej ilości obliczeń. \nOstatni obliczony punkt wynosi : {lastPoint} \nWartość : { _functionDefinition.GetValue(lastPoint)}");

            }
            catch (Exception ex )
            {
                _messageDialogService.ShowInfoDialog(ex.ToString());
            }
        }
        private void UpdateDataTable(IEnumerable<Point> points)
        {
            var newDatatable = new DataTable();
            var PointNo = 1;
            var Comma = DigitsAfterCommaBasedOnEpsilon1();
            newDatatable.Columns.Add("No");

            foreach (var arg in Variables)
            {
                newDatatable.Columns.Add(arg.VariableName);
            }
            newDatatable.Columns.Add("Wartość");

            foreach (var point in points)
            {
                var argValue = point.ListOfVariables;

                var obj = new object[newDatatable.Columns.Count];
                obj[0]= PointNo++;
                int lastIndex = newDatatable.Columns.Count - 1;

                for (int i = 0; i < argValue.Length; i++)
                {
                    obj[i+1] = string.Format($"{{0:N{Comma}}}", argValue[i]); 
                }
                obj[lastIndex] = string.Format($"{{0:N{Comma}}}", point.FunctionValue);

                newDatatable.LoadDataRow(obj, false);
            }

            DataTable = newDatatable;
        }
        private bool IsArgumentChanged(org.mariuszgromada.math.mxparser.Function oldfun)
        {
            var noOldParameters = oldfun.getParametersNumber();
            var noNewParameters = _functionDefinition.Function.getParametersNumber();

            if (noOldParameters != noNewParameters)
                return true;

            for (int i = 0; i < noNewParameters; i++)
            {
                if (oldfun.getArgument(i) != _functionDefinition.Function.getArgument(i))
                    return true;
            }
            return false;

        }
        private void UpdateVariablees()
        {
            var NoOfVariables = _functionDefinition.Function.getArgumentsNumber();

            if (NoOfVariables != Variables.Count())
            {
                Variables.Clear();
                for (int i = 0; i < NoOfVariables; i++)
                {
                    Variables.Add(new Variable { VariableName = _functionDefinition.Function.getArgument(i).getArgumentName(), Value = 0 });
                }
            }
        }
        private int DigitsAfterCommaBasedOnEpsilon1()
        {
            int p = 0;
            var val = InputParameters.Epsilon1;

            if (val >= 1)
                return 0;

            while (val <1)
            {
                val *= 10;
                p++;
            }

            return p;
        }
        private void UpdateStartedPoint()
        {
            var variablesValue = new double[Variables.Count()];

            for (int i = 0; i < variablesValue.Count(); i++)
            {
                variablesValue[i] = Variables[i].Value;
            }
            Xo.ListOfVariables = variablesValue;
            
        }


    }
}
