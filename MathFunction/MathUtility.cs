using System;
using System.Collections.Generic;
using System.Linq;

namespace MathFunction
{
    public static class MathUtility
    {
        public static double Epsilon { get; set; }

        public static void SetEpsilon(double epsilon)
        {
            Epsilon = epsilon;
        }
        public static Point MovePoint(Point x, IEnumerable<double> d)
        {
            if (x.ListOfVariables.Count() != d.Count())
                throw new IncorrectDimensionException();

            var variables = x.ListOfVariables;
            var newVariables = new double[x.ListOfVariables.Length];

            for (int i = 0; i < variables.Length; i++)
                newVariables[i] = variables[i] + d.ElementAt(i);
            return new Point(newVariables);
        }
        public static IEnumerable<double> DirectionD(IEnumerable<double> gradient)
        {
            foreach (var g in gradient)
                yield return -g;
        }
        public static IEnumerable<double> Gradient(FunctionDefinition func, Point Xo)
        {
            var derivative = new Derivative(func);
            for (int i = 0; i < Xo.ListOfVariables.Length; i++)
            {
                var der = derivative.FirstDerivativeInPoint(Xo, i);
                yield return der;
            }
        }
        public static IEnumerable<double> Multiplication(IEnumerable<double> vs, double val)
        {
            foreach (var v in vs)
                yield return v * val;
        }

        public static double DerivateryInDirection(IEnumerable<double> gradient, IEnumerable<double> direction)
        {
            if (gradient.Count() != direction.Count())
                throw new FormatException("Incorrect gradient or direction");
            double p = 0;
            for (int i = 0; i < gradient.Count(); i++)
                p += gradient.ElementAt(i) * direction.ElementAt(i);
            return p;
        }
        public static double ScalarProduct(IEnumerable<double> vect1, IEnumerable<double> vect2)
        {
            if(vect1.Count() != vect2.Count())
                throw new IncorrectDimensionException();
            double result = 0;
            for (int i = 0; i < vect1.Count(); i++)
                result += vect1.ElementAt(i) * vect2.ElementAt(i);
            return result;
        }
        public static double VectorLength(IEnumerable<double> vector)
        {
            var tmp = vector.Select(v => v * v).ToList().Sum();
            return Math.Sqrt(tmp);
        }
        public static double Abs(double val) => Math.Abs(val);
        public static double Floor(double data) => System.Math.Floor(data * 1 / Epsilon) * Epsilon;

    }
}
