using System.Collections.Generic;


namespace Algorithms
{
    using MathFunction;
    using System.Linq;

    public class TheBisectionAlgorithmWithGoldsteinTwoSidedTest
    {
        private FunctionDefinition function;
        private Point _xo;
        private Point Xo
        {
            get { return _xo; }
            set
            {
                _xo = value;
                fx0 = function.GetValue(Xo);
            }
        }
        private IEnumerable<double> direction;
        private readonly double beta;
        private readonly double epsilon;
        private IEnumerable<double> tauD; //Direction*tau
        private double fx0;
        private double fx0Taud;
        private double p;
        public double Tau { get; set; }
        public TheBisectionAlgorithmWithGoldsteinTwoSidedTest
            (FunctionDefinition func, Point x0, IEnumerable<double> D, double tauR, double beta, double e=0.000001)
        {
            function = func;
            Xo = x0;
            direction = D.ToList();
            this.beta = beta;
            Tau = tauR;
            epsilon = e;
            tauD = MathUtility.Multiplication(D, tauR);
        }
        public TheBisectionAlgorithmWithGoldsteinTwoSidedTest(FunctionDefinition func, double tauR, double beta, double e = 0.000001)
        {
            function = func;
            this.beta = beta;
            Tau = tauR;
            epsilon = e;
        }
        public void SetDirection(Point x0, IEnumerable<double> D,IEnumerable<double> g)
        {
            Xo = x0;
            direction = D.ToArray();
            fx0Taud= function.GetValue(MathUtility.MovePoint(Xo, MathUtility.Multiplication(direction, Tau)));
            p = MathUtility.DerivateryInDirection(g, direction);
        }
        public Point Run()
        {
            double rightSide = 0;
            double fx0taud = 0;
            var t = Tau;
            var tauR = Tau;
            var tauL = 0.0;
            //1)    
            do
            {
                //2)
                tauD = MathUtility.Multiplication(direction, t);
                fx0taud = function.GetValue(MathUtility.MovePoint(Xo, tauD));

                //3)
                rightSide = fx0 + (1 - beta) * p * t;

                if(fx0taud < rightSide)
                {
                    tauL = t;
                    if (tauL == tauR) throw new InvalidTauException();
                }
                else
                {
                    //4)
                    rightSide = fx0 + beta * p * t;
                    if(fx0taud > rightSide)
                        tauR = t;                        
                    else
                        return MathUtility.MovePoint(Xo, tauD);
                }
                t = CalculateTau(tauL, tauR);

            } while (true);
        }
        private double CalculateTau(double tl, double tr)
        {
           return 0.5 * (tl + tr);
        }
        public bool TwoSideTest()
        {
            var rightSide_1 = fx0 + (1 - beta) * p * Tau;
            var rightSide_2 = fx0 + beta * p * Tau;

            if (fx0Taud < rightSide_1)
                return false;
            if (fx0Taud > rightSide_2)
                return false;
            return true;
        }
    }
}
