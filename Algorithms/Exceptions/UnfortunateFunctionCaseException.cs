using MathFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class UnfortunateFunctionCaseException : Exception
    {
        public IEnumerable<Point> Points { get; set; }
        public UnfortunateFunctionCaseException(IEnumerable<Point> alreadyCalculatedPoints, string message = null) : base(message)
        {
            Points = alreadyCalculatedPoints;
        }
    }
}
