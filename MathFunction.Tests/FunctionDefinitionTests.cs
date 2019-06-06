using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MathFunction.Tests
{
    public class FunctionDefinitionTests
    {
        [TestCase(1,2, -3)]
        [TestCase(2, 5, 2)]
        public void GetValueTests(double val1, double val2, double effect )
        {
            var func = new FunctionDefinition("f(x1,x2) = (x1)^2 - 6*x1 +x1*x2");
            var point = new Point(val1, val2);
            Assert.AreEqual(effect, func.GetValue(point));
        }
        [Test]
        public void CheckNumebers()
        {
            var func = new FunctionDefinition("f(x,y,z,a) = x+y+z + x*y");

            var no = func.Function.getArgumentsNumber();
            var arg = func.Function.getArgument(0);
            var fname = func.Function.getFunctionName();
            var argno = func.Function.getParametersNumber();
        }
    }
}
