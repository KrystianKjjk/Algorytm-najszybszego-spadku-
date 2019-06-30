using ChartsCreator;
using FastestFallProgramExample.Events;
using FastestFallProgramExample.Model;
using MathFunction;
using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FastestFallProgramExample.ViewModel
{
    public class CounterLineViewModel :  PropertyChangedBase, ICounterLineViewModel
    {
        private IEventAggregator _eventAggregator;

        private readonly double _calculatedPointSize;
        private string _function;
        private BitmapSource _bitmap;
        private double _BitmapSize;
        private double _XLegend;
        public CounterLineViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _calculatedPointSize = 6 ;
            BitmapSize = 100;

            PointItems = new ObservableCollection<PointItem>();
            LinesConnectingPoints = new ObservableCollection<LineItem>();

            _eventAggregator.GetEvent<CreateCounterLineEvent>().Subscribe(CreateCounterLine);

        }
        public void Load(IEnumerable<Point> points, string function)
        {
            _function = function;
            CalculatedPoints = points;
        }
        public PointItem CentralPoint { get; set; }
        public IEnumerable<Point> CalculatedPoints { get; private set; }
        public ObservableCollection<PointItem> PointItems { get; set; }
        public ObservableCollection<LineItem> LinesConnectingPoints { get; set; }
        public BitmapSource Bitmap
        {
            get { return _bitmap; }
            set
            {
                _bitmap = value;
                OnPropertyChanged();
            }
        }
        public double BitmapSize
        {
            get { return _BitmapSize; }
            set
            {
                _BitmapSize = value;
                XLegend = value - 30;
                OnPropertyChanged();
            }
        }
        public double XLegend
        {
            get { return _XLegend; }
            set
            {
                _XLegend = value;
                OnPropertyChanged();
            }
        }
        public async void Create(double imageSize, double range, int step, double CentrumX = 0, double CentrumY = 0)
        {
            _eventAggregator.GetEvent<CreateCounterLineInfoEvent>().Publish(true); // Counterine is Creatng
            BitmapSize = imageSize;
            var pixelsCaclulator = new PixelsCaclulator(imageSize, imageSize, range, step, CentrumX, CentrumY);

            PointItems.Clear();
            LinesConnectingPoints.Clear();

            var centralCoordinate = pixelsCaclulator.CalculatePixel(CentrumX, CentrumY);

            CentralPoint = new PointItem( centralCoordinate, new Point(CentrumX, CentrumY),_calculatedPointSize);
            var pixels = await GetPixelsTask(pixelsCaclulator);
            Bitmap = BitmapCreator.CreateBitmap(pixels);

            _eventAggregator.GetEvent<CreateCounterLineInfoEvent>().Publish(false); // Counterine is not Creatng
            CalculatePointItemBasemOnCalculatedPoints(CalculatedPoints, pixelsCaclulator);

        }
        private async Task<IEnumerable<Pixel>> GetPixelsTask (PixelsCaclulator pixelsCalculator)
        {
            return await Task.Run(() => pixelsCalculator.CalculatePixelsForCounterLine(_function));
            
        }
        private void CreateCounterLine(CreateCounterLineEventArgs obj)
        {
            if (obj.Function != null)
                _function = obj.Function;
            if(obj.Points != null)
                CalculatedPoints = obj.Points;
            BitmapSize = obj.ImegeSize;
            Create(obj.ImegeSize, obj.Range, obj.Step, obj.X, obj.Y);
            
        }
        private void CalculatePointItemBasemOnCalculatedPoints(IEnumerable<Point> points, PixelsCaclulator parametrizedPixelsCaclulator)
        {
            Coordinates GetCalculatedPixels(Point vs)
            {
                var coor = parametrizedPixelsCaclulator.CalculatePixel(vs.ListOfVariables[0], vs.ListOfVariables[1]);
                coor.X -= _calculatedPointSize / 2; // change the coordinates to put pixel in the center of the point  
                coor.Y -= _calculatedPointSize / 2;
                return coor;
            }

            void ConnectPointWithLine(PointItem A, PointItem B)
            {
                var CoorA = new Coordinates(A.Coordinates.X + _calculatedPointSize / 2, A.Coordinates.Y + _calculatedPointSize / 2);
                var CoorB = new Coordinates(B.Coordinates.X + _calculatedPointSize / 2, B.Coordinates.Y + _calculatedPointSize / 2);

                var lineItem = new LineItem(CoorA, CoorB);
                LinesConnectingPoints.Add(lineItem);
            }

            if (points == null || points.Count() == 0)
                return;

            var point = points.ElementAt(0);

            var coordinates = GetCalculatedPixels(point);

            var PointItem = new PointItem(coordinates, point, _calculatedPointSize);

            PointItems.Add(PointItem);

            for (int i = 1; i < points.Count(); i++)
            {
                point = points.ElementAt(i);
                coordinates = GetCalculatedPixels(point);
                PointItem = new PointItem(coordinates, point, _calculatedPointSize);
                PointItems.Add(PointItem);


                ConnectPointWithLine(PointItems.ElementAt(i - 1), PointItem);
            }
        }
    }
}