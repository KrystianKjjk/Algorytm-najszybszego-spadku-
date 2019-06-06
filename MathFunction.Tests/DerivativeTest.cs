using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MathFunction.Tests
{
    public class DerivativeTest
    {
        [Test]
        public void DerivativeInPointTest()
        {
            var derivatve = new Derivative("f(x) = x*x");

            var point = new Point("2");

            var der = derivatve.FirstDerivativeInPoint(point, 0);

            if (Math.Abs(der - 4) < 0.0001)
            {
                Assert.Pass();
            }
            else
            {
                Assert.AreEqual(4, der);
            }
        }
        [Test]
        public void DerivativeInPointTestV2()
        {
            var derivatve = new Derivative("f(x1, x2) = x1*x1 + x2*x2");

            var point = new Point("2,3");

            var der = derivatve.FirstDerivativeInPoint(point, 0);

            if (Math.Abs(der - 4) < 0.0001)
            {
                Assert.Pass();
            }
            else
            {
                Assert.AreEqual(4, der);
            }
        }
        [TestCase("2", "3",0 ,3)]
        [TestCase("2", "3",1, 8)]
        [TestCase("2.44", "3.34", 1, 9.12)]
        public void DerivativeInPointTestV3(string p1, string p2, int No, double result)
        {
            var derivatve = new Derivative("f(x1, x2) = x1*x2 + x2*x2");

            var point = new Point($"{p1},{p2}");

            var der = derivatve.FirstDerivativeInPoint(point, No);

            if (Math.Abs(der - result) < 0.0001)
            {
                Assert.Pass();
            }
            else
            {
                Assert.AreEqual(result, der);
            }
        }
        [TestCase("1", "1", "1", 0,0, 2)]
        [TestCase("2", "3", "3", 1,2, 3)]
        [TestCase("2.44", "3.34", "3.34", 1,0, 0)]
        [TestCase("2.44", "3.34", "3.34", 2, 1, 3)]
        public void DerivativeInPointTestSecondV3(string p1, string p2, string p3, int No, int No2, double result)
        {
            var derivatve = new Derivative("f(x1, x2, x3) = x1^2+ 2*x2*x3+x3*x2");

            var point = new Point($"{p1},{p2}, {p3}");

            var der = derivatve.SecondDerivativeInPoint(point, No, No2);

            if (Math.Abs(der - result) < 0.0001)
            {
                Assert.Pass();
            }
            else
            {
                Assert.AreEqual(result, der);
            }
        }
    }
}
