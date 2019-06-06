using MathFunction;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace ChartsCreator
{
    public class Pixel
    {
        public Point Point { get; set; }
        public Coordinates PixelCoordinates { get; set; }
        public Color PixelColor { get; set; }
        public Pixel(Point point, int x, int y)
        {
            Point = point;
            PixelCoordinates = new Coordinates(x, y);
        }
        public Pixel(int x, int y)
        {
            PixelCoordinates = new Coordinates(x, y);
        }
        public void CalculateSetColor(double value, double MinScalar, double MaxScalar)
        {
            var H = (value - MinScalar)/(MaxScalar - MinScalar);
            H *= 270;
            var newColor = new Color();

            var rgb = newColor.HsvToRGB((int)H);
            newColor.R = rgb.B;
            newColor.G = rgb.G;
            newColor.B = rgb.R;

            PixelColor = newColor;
        }

        public override string ToString()
        {
            return $"{PixelCoordinates.X} : {PixelCoordinates.Y} :: {PixelColor}";
        }
    }
}
