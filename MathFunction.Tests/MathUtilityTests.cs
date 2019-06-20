using MathFunction;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class MathUtilityTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DirectionDTest()
        {
            var gradient = new List<double>
            {
                2,4,3.44,-23,-4.44
            };
            var d = MathUtility.DirectionD(gradient);

            var result = new List<double>
            {
                -2,-4,-3.44,23,4.44
            };
            Assert.AreEqual(result, d);            
        }
        [Test]
        public void gradientTest()
        {
            var function = new FunctionDefinition("f(x1,x2) = (x1-2)^2 -(x2-1)^2");
            var point = new Point("1,1");

            var gradient = MathUtility.Gradient(function, point).ToList();

            var result = new List<double> {-2,0 };

            if (gradient[0] - result[0] < 0.00001 && gradient[1] - result[1] < 0.00001)
            {
                Assert.Pass();
            }
            else
            {
                Assert.AreEqual(result, gradient);
            }

        }
        [Test]
        public void SumVectorsTest()
        {
            Point P = new Point("1,2,3");
            var d = new List<double>
            {
                3,4,5
            };
            var x = MathUtility.MovePoint(P, d);

            var result = new List<double>
            {
                4,6,8
            };

            Assert.AreEqual(result, x.ListOfVariables);
        }
        [Test]
        public void VectorLengthTest()
        {
            List<double> vector = new List<double> { 2, 3 };
            var result = MathUtility.VectorLength(vector);

            var expected = Math.Sqrt(13);
            Assert.AreEqual(expected, result);
        }



    }
}