using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NUnit.Framework;

namespace MathFunction.Tests
{
    public class PointTest
    {
        private readonly NumberFormatInfo info = new CultureInfo("en-US", false).NumberFormat;
        [TestCase(1.3223, 2.32, 3)]
        [TestCase(1.33, 33, 3)]
        [TestCase(1, 2, 3)]
        public void ListOfVariablesTest(double val1, double val2, double val3)
        {
            var point = new Point
                (val1, val2, val3);

            var effect = new List<double>
            {
                val1,val2,val3
            };
            Assert.AreEqual(effect, point.ListOfVariables);
        }
        [Test]
        public void OperatorTest()
        {
            var P1 = new Point("1,2.3,4");
            var P2 = new Point("1,2.3,4");

            Assert.IsTrue(P1 == P2);
        }
        [Test]
        public void ChangePointValueForrStringTest()
        {
            var X = new Point("1,3.55,4");

            X.ChangePointValue(99.0, 0);

            Assert.AreEqual("99,3.55,4", X.ToString());
        }
        [Test]
        public void ChangePointValueForDoubleTest()
        {
            var X = new Point("1,3.44,4");

            X.ChangePointValue(4, 0, OperationType.Replace);

            Assert.AreEqual("4,3.44,4", X.ToString());
        }
        [Test]
        public void PointsDifference()
        {
            var A = new Point("3,5");
            var B = new Point("1,3");
            var result = Point.PointsDifference(A, B);

            var expected = new List<double> { 2, 2 };
            Assert.AreEqual(expected, result);
        }
    }


}
