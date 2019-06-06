using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFunction
{
    public class IncorrectDimensionException : Exception
    {
        public IncorrectDimensionException(string message = null) : base(message)
        {
        }
    }
}
