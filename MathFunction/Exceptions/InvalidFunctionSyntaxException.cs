using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFunction
{
    public class InvalidFunctionSyntaxException : Exception
    {
        public InvalidFunctionSyntaxException(string message = null) : base(message)
        {
        }
    }
}
