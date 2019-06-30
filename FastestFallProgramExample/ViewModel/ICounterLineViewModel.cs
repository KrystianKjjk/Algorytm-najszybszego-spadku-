using MathFunction;
using System.Collections.Generic;

namespace FastestFallProgramExample.ViewModel
{
    public interface ICounterLineViewModel
    {
        void Load(IEnumerable<Point> points, string function);
        void Create(double Imagesize, double range, int step = 10, double CentrumX = 0, double CentrumY = 0);
    }
}