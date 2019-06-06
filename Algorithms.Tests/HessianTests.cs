using System;
using MathFunction;
using NUnit.Framework;

namespace Algorithms.Tests
{
    public class HessianTests
    {
        [TestCase(1,1,1, 6)]
        public void CalculateHessianTests(double p1, double p2, double p3, double result)
        {
            var func = new FunctionDefinition("f(x1, x2, x3) = x1^3 + 2 * x2 * x3^2 + x3 * x2 + x3^3");

            var point = new Point(p1, p2, p3);

            var der = Hessian.CalculateHessian(func, point);

            if (Math.Abs(der[0,0] - result) < 0.0001 && Math.Abs(der[1, 2] - 5) < 0.0001)
            {
                Assert.Pass();
            }
            else
            {
                Assert.AreEqual(result, der);
            }
        }
        [TestCase]
        public void CheckHessianDeterminantsTests()
        {
            var func = new FunctionDefinition("f(x1,x2) = x1^2 +x2^2");
            var point = new Point(5, 4);

            var StatCondition = Hessian.ChceckHessianDeterminants(func, point);

            Assert.AreEqual(Hessian.StationaryConditions.Minimum, StatCondition);
        }
    }
}
