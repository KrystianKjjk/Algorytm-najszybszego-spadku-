using MathFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastestFallProgramExample.Model
{
    public class InputParameters
    {
        public double Tau { get; set; }
        public double Beta { get; set; }
        public double Epsilon1 { get; set; }
        public double Epsilon2 { get; set; }
        public double Epsilon3 { get; set; }
        public int LIteration { get; set; }
        public InputParameters()
        {
            Tau = 1;
            Beta = 0.4;
            LIteration = 200;
            Epsilon1 = 0.00001;
            Epsilon2 = 0.00001;
            Epsilon3 = 0.00001;
        }
    }
}
