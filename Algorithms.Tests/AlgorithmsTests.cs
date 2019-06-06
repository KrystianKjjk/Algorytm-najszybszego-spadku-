using MathFunction;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Algorithms.Tests
{
    public class AlgorithmsTests
    {
        [Test]
        public void TheBisectionAlgorithmTest()
        {
            var func = new FunctionDefinition("f(x1,x2) = (x1)^2 + 2*(x2)^2 - 6*x1 + x1*x2");
            var D = new List<double> { 1, 0 };
            var point = new Point("0,0");
            var beta = 0.4;
            var tauR = 9;
            var epsilon = 0.00001;
            var algorithm = new TheBisectionAlgorithmWithGoldsteinTwoSidedTest
                (func, point, D,tauR,beta ,epsilon);
            var result = algorithm.Run();
            var expected0 = 6;
            var expected1 = 0;
            if (Math.Abs(result.ListOfVariables[0] - expected0) <= epsilon && Math.Abs(result.ListOfVariables[1] - expected1) <= epsilon)
            {
                Assert.True(true);
            }
            else
            {
                Assert.False(true);
            }

        }
        [Test]
        public void TheBisectionAlgorithmTestV2()
        {
            var func = new FunctionDefinition("f(x1,x2) = 2*(x1)^2+(x2)^2 -2*x1*x2");
            var point = new Point("2,3");
            var D = MathUtility.Gradient(func, point);
            D = MathUtility.DirectionD(D);
            var beta = 0.25;
            var tauR = 0.1;
            var epsilon = 0.00001;
            var algorithm = new TheBisectionAlgorithmWithGoldsteinTwoSidedTest
                (func, point, D, tauR, beta, epsilon);

            Assert.Throws<InvalidTauException>(() =>algorithm.Run());
        }
        [Test]
        public void NsTest()
        {
            //var func = "f(x1,x2) = 2*(x1)^2 + (x2)^2 -2*x1*x2";
            var func = "f(x1,x2) = (x1-2)^2+(x1-x2^2)^2";
            //var func = "f(x1,x2) = x1^2 + x1*x2 + 0.5*x2^2 - x1 - x2";
            // var func = "f(x1,x2) = (x1-2)^2 - (x2-1)^2";
            //var func = "f(x1,x2,x3,x4) = 100*((x2-x1^2)^2+(x1-1)^2+90*(x4-x3^2)^2+(1-x3)^2+10.1*((x2-1)^2+(x4-1)^2)+19.8*(x2-1)*(x4-1))";
            //var func = "f(x) = ((x^2))";
            var Xo = new Point("1,-3");
            
            var beta = 0.25;
            var tau = 0.5;
            var l = 200;
            var Ns = new FastestFallAlgorithm(func, Xo, beta, tau, l);


            FunctionDefinition function = new FunctionDefinition(func);
            var fx = function.GetValue(Xo);
            Ns.Run();

            var results = new List<double>();
            foreach (var point in Ns.Points)
            {
                results.Add(function.GetValue(point));
            }
        }
        [Test]
        public void NsTestShouldReturnTauException()
        {
            var func = "f(x1,x2) = (x1-2)^2+(x1-x2^2)^2";
            var Xo = new Point("1,-3");

            var beta = 0.25;
            var tau = 0.005;
            var l = 200;
            var Ns = new FastestFallAlgorithm(func, Xo, beta, tau, l);


            FunctionDefinition function = new FunctionDefinition(func);
            var fx = function.GetValue(Xo);
            Assert.Throws<InvalidTauException>(()=> Ns.Run());
        }
    }
}
