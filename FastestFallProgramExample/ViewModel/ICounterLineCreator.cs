using MathFunction;
using System.Collections.Generic;

namespace FastestFallProgramExample.ViewModel
{
    public interface ICounterLineCreator
    {
        void Create(double Imagesize, double range, int step = 10, double CentrumX = 0, double CentrumY = 0);
    }
}