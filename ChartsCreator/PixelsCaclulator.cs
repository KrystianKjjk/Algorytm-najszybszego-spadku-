using MathFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartsCreator
{
    public class PixelsCaclulator
    {
        public double ImageWidth { get; }
        public double ImageHeight { get; }
        public double Range { get; }
        public int Step { get; }
        public double StartX { get; }
        public double StartY { get; }

        public PixelsCaclulator(double width, double height, double range, int step = 10, double startX = 0, double startY = 0)
        {
            ImageWidth = width;
            ImageHeight = height;
            Range = range;
            Step = step;
            StartX = startX;
            StartY = startY;
        }
        public IEnumerable<Pixel> CalculatePixelsForCounterLine(string functionString)
        {
            var p = (2 * Range) / (ImageWidth / Step);

            var function = new MathFunction.FunctionDefinition(functionString);

            var values = new List<Pixel>();
            double X;
            double Y;
            double effect;
            for (int y = 0, a = 0; y < ImageHeight; y = y + Step, a++)
            {
                for (int x = 0, b = 0; x < ImageWidth; x = x + Step, b++)
                {
                    X = -Range + b * p + StartX;
                    Y = Range - a * p + StartY;

                    effect = function.GetValue(X, Y);
                    var newPoint = new Point(X, Y);
                    newPoint.FunctionValue = effect;
                    values.Add(new Pixel(newPoint ,x, y));
                }

            }
            var maxValue = values.Max(pix=>pix.Point.FunctionValue);
            var minValue = (from val in values where !double.IsNaN((double)val.Point.FunctionValue) select val).Min(pix=>pix.Point.FunctionValue);

            foreach (var value in values)
            {
                value.CalculateSetColor((double)value.Point.FunctionValue, (double)minValue, (double)maxValue);
            }
            return values;
        }
        public Coordinates CalculatePixel(double VariableX, double VariableY)
        {
            var p = (2 * Range) / (ImageWidth / Step);
            double X, Y;

            X = (VariableX + Range - StartX) / (2 * Range / ImageWidth);
            Y = (VariableY - Range - StartY) / (-2 * Range / ImageWidth);

            return new Coordinates(X,Y);
        }
    }

}
