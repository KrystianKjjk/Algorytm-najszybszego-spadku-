using ChartsCreator;
using MathFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastestFallProgramExample.Model
{
    public class PointItem
    {
        public Coordinates Coordinates { get; set; }
        public Point Point { get; set; }
        public double Size { get; set; }
        public PointItem(Coordinates coordinates, Point point, double size)
        {
            Coordinates = coordinates;
            Point = point;
            Size = size;

        }
    }
}
