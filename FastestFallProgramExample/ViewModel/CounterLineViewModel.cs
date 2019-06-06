using ChartsCreator;
using FastestFallProgramExample.Events;
using FastestFallProgramExample.Model;
using MathFunction;
using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;

namespace FastestFallProgramExample.ViewModel
{
    public class CounterLineViewModel :  PropertyChangedBase, ICounterLineCreator
    {
        private IEventAggregator _eventAggregator;

        private readonly double _pointSize;
        private readonly string _function;
        private BitmapSource _bitmap;
        private double _BitmapSize;

        public CounterLineViewModel(IEventAggregator eventAggregator, IEnumerable<Point> points, string function)
        {
            _eventAggregator = eventAggregator;

            _pointSize = 6 ;
            _function = function;
            BitmapSize = 100;
            CalculatedPoints = points;

            PointItems = new ObservableCollection<PointItem>();
            LinesConnectingPoints = new ObservableCollection<LineItem>();

            _eventAggregator.GetEvent<CreateCounterLineEvent>().Subscribe(CreateCounterLine);



        }

        public PointItem CentralPoint { get; set; }
        public IEnumerable<Point> CalculatedPoints { get; }
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
        public double XLegend { get; set; }
        public void Create(double imageSize, double range, int step, double CentrumX = 0, double CentrumY = 0)
        {
            BitmapSize = imageSize;
            var pixelsCaclulator = new PixelsCaclulator(imageSize, imageSize, range, step, CentrumX, CentrumY);

            PointItems.Clear();
            LinesConnectingPoints.Clear();

            var centralCoordinate = pixelsCaclulator.CalculatePixel(CentrumX, CentrumY);

            CentralPoint = new PointItem( centralCoordinate,new Point(CentrumX, CentrumY),_pointSize);
             var pixels = pixelsCaclulator.CalculatePixelsForCounterLine(_function);
            Bitmap = BitmapCreator.CreateBitmap(pixels);
            CalculatePointItemBasemOnCalculatedPoints(CalculatedPoints, pixelsCaclulator);

           // var minPixelValue = (double)pixels.Min(p=>p.Point.FunctionValue); // blue 
           // var maxpixelValue = (double)pixels.Max(p=>p.Point.FunctionValue); // red 

           // var legendStep = (maxpixelValue - minPixelValue) / 100;

           // var legendPixels = new List<Pixel>();
           // for (double i = minPixelValue; i < maxpixelValue; i = i+legendStep)
           // {
           //     var p = new Pixel(50,10);
           //     p.CalculateSetColor(i, minPixelValue, maxpixelValue);
           //     legendPixels.Add(p);
           // }
           // Bitmap = BitmapCreator.CreateBitmap(legendPixels, (int)BitmapSize, (int)BitmapSize);
           //Bitmap = BitmapCreator.CreateBitmap(legendPixels);

        }

        private void CreateCounterLine(CreateCounterLineEventArgs obj)
        {
            Create(obj.ImegeSize, obj.Range, obj.Step, obj.X, obj.Y);
            BitmapSize = obj.ImegeSize;
        }
        private void CalculatePointItemBasemOnCalculatedPoints(IEnumerable<Point> points, PixelsCaclulator parametrizedPixelsCaclulator)
        {
            Coordinates GetCalculatedPixels(Point vs)
            {
                var coor = parametrizedPixelsCaclulator.CalculatePixel(vs.ListOfVariables[0], vs.ListOfVariables[1]);
                coor.X -= _pointSize / 2; // change the coordinates to put pixel in the center of the point  
                coor.Y -= _pointSize / 2;
                return coor;
            }

            void ConnectPointWithLine(PointItem A, PointItem B)
            {
                var CoorA = new Coordinates(A.Coordinates.X + _pointSize / 2, A.Coordinates.Y + _pointSize / 2);
                var CoorB = new Coordinates(B.Coordinates.X + _pointSize / 2, B.Coordinates.Y + _pointSize / 2);

                var lineItem = new LineItem(CoorA, CoorB);
                LinesConnectingPoints.Add(lineItem);
            }

            if (points == null || points.Count() == 0)
                return;

            var point = points.ElementAt(0);

            var coordinates = GetCalculatedPixels(point);

            var PointItem = new PointItem(coordinates, point, _pointSize);

            PointItems.Add(PointItem);

            for (int i = 1; i < points.Count(); i++)
            {
                point = points.ElementAt(i);
                coordinates = GetCalculatedPixels(point);
                PointItem = new PointItem(coordinates, point, _pointSize);
                PointItems.Add(PointItem);

                ConnectPointWithLine(PointItems.ElementAt(i - 1), PointItem);
            }
        }
    }
}