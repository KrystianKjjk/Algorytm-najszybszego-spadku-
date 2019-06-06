

namespace Algorithms
{
    using MathFunction;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Hessian
    {
        public static double[,] CalculateHessian(FunctionDefinition function, Point Xo, double epsilon = 0.00001)
        {
            var Dim = Xo.ListOfVariables.Length;
            var H = new double[Dim, Dim];
            var der = new Derivative(function, epsilon);
            for (int i = 0; i < Dim; i++)
            {
                for (int j = 0; j < Dim; j++)
                {
                    H[i, j] = der.SecondDerivativeInPoint(Xo, i, j);
                }
            }
            return H;
        }
        public static double DeterminantHessian(double[,] hessian)
        {
            return Determinant.Det(hessian);
        }
        public static double DeterminantHessian(FunctionDefinition function, Point Xo, double epsilon = 0.00001)
        {
            return DeterminantHessian(CalculateHessian(function, Xo, epsilon));
        }
        public static StationaryConditions ChceckHessianDeterminants(FunctionDefinition function, Point Xo, double epsilon = 0.00001)
        {
            bool ChceckResult(IEnumerable<bool> results)
            {
                return results.All(d => d == true);
            }

            var hessian = CalculateHessian(function, Xo, epsilon);

            var HessLength = Math.Sqrt(hessian.Length);


            var determinans = new List<double>();
            for (int i = 1; i <= HessLength; i++)
            {
                double[,] A;
                if (i == HessLength)
                    A = hessian;
                else
                {
                    A = new double[i, i];

                    for (int k = 0; k < i; k++)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            A[k, j] = hessian[k, j];
                        }
                    }
                }
                determinans.Add(DeterminantHessian(A));
            }
            //1)
            {
                var results = determinans.Select(d => d > 0).ToList();

                if (ChceckResult(results) == true)
                    return StationaryConditions.Minimum;
            }
            //2)
            {
                var results = new List<bool>();
                for (int i = 0; i < determinans.Count(); i++)
                {
                    results.Add(determinans[i] * Math.Pow(-1, i) > 0);
                }
                if (ChceckResult(results) == true)
                    return StationaryConditions.Maxsimum;
            }
            //3)
            var DetAn = determinans.Last();
            {

                var results = determinans.Select(d => d >= 0);

                if (ChceckResult(results) && DetAn == 0)
                    return StationaryConditions.UndefinedExtremum;
            }

            {
                var results = new List<bool>();
                for (int i = 0; i < determinans.Count() - 1; i++)
                {
                    results.Add(determinans[i] * Math.Pow(-1, i) >= 0);
                }
                if (ChceckResult(results) && DetAn == 0)
                    return StationaryConditions.UndefinedExtremum;
            }
            return StationaryConditions.ExtremumAbsense;

        }
        public enum StationaryConditions
        {
            Minimum,
            Maxsimum,
            UndefinedExtremum,
            ExtremumAbsense
        }
    }
}
