using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    using MathFunction;
    public class FastestFallAlgorithm
    {
        public FunctionDefinition Func { get; }
        public List<Point> Points { get; set; }
        public int LIteration { get; set; }
        public double Beta { get; set; }
        public double Tau { get; set; }
        public double Epsilon { get; set; }
        public double Epsilon1 { get; set; }
        public double Epsilon2 { get; set; }
        public FastestFallAlgorithm(FunctionDefinition function, Point Xo, double beta, double tau, int l, double epsilon = 0.00001, double epsilon1 = 0.00001, double epsilon2 = 0.00001)
        {
            Func = function;
            FastestFallAlgorithmStartup(Xo, beta, tau, l, epsilon, epsilon1, epsilon2 );
        }
        public FastestFallAlgorithm(string function, Point Xo, double beta, double tau, int l,double epsilon = 0.00001, double epsilon1 = 0.00001, double epsilon2 = 0.00001)
        {
            Func = new FunctionDefinition(function);
            FastestFallAlgorithmStartup(Xo, beta, tau, l, epsilon, epsilon1, epsilon2);
        }
        private void FastestFallAlgorithmStartup(Point Xo, double beta, double tau, int l, double epsilon = 0.00001, double epsilon1 = 0.00001, double epsilon2 = 0.00001)
        {
            Xo.FunctionValue = Func.GetValue(Xo);
            Points = new List<Point> { Xo };
            Beta = beta;
            Tau = tau;
            LIteration = l;
            Epsilon = epsilon;
            Epsilon1 = epsilon1;
            Epsilon2 = epsilon2;
        }
        public List<Point> Run()
        {
            var x = Points[0];
            var goldstein = new TheBisectionAlgorithmWithGoldsteinTwoSidedTest(Func, Tau, Beta, Epsilon);
            IEnumerable<double> D;
            do
            {
                //1)
                var gradient = MathUtility.Gradient(Func, x).ToArray();
                //2)
                Point pointB = null;
                if(Points.Count > 1)
                    pointB = Points[Points.Count - 2];
                if (ISStop(gradient, Points.Last(), pointB))
                {
                    return Points;
                }
                else
                {
                    //3)
                    D = MathUtility.DirectionD(gradient).ToArray();
                    goldstein.SetDirection(x, D, gradient);
                    if (goldstein.TwoSideTest() == false)
                        x = goldstein.Run();
                    else
                        x = MathUtility.MovePoint(x, MathUtility.Multiplication(D, Tau));

                    x.FunctionValue = Func.GetValue(x);
                    Points.Add(x);
                }
            } while (true);
        }
        public bool ISStop(IEnumerable<double> gradient, Point A, Point B = null)
        {
            return IsSclalarOfGradientEqualTo0(gradient)
                || IsPointsDiffEqualTo0(A, B)
                || IsFuncValueEqualTo0(A, B)
                || Points.Count >= LIteration;
        }
        private bool IsSclalarOfGradientEqualTo0(IEnumerable<double> gradient)
        {
            return MathUtility.ScalarProduct(gradient, gradient) <= Epsilon;

        }
        private bool IsPointsDiffEqualTo0(Point A, Point B = null)
        {
            if (B == null) return false;

            var VectorLength = MathUtility.VectorLength(Point.PointsDifference(A, B));
            return VectorLength <= Epsilon1;
        }
        private bool IsFuncValueEqualTo0(Point A, Point B = null)
        {
            if (B == null) return false;

            var result = MathUtility.Abs(Func.GetValue(A) - Func.GetValue(B));
            return result <= Epsilon2;
        }
    }
}
