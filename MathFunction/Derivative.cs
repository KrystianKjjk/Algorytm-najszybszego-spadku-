using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;

namespace MathFunction
{
    public class Derivative
    {
        private readonly double precision = 0.001;
        private Expression _derivativeExpression;

        public Derivative(string definition, double precision = 0.00001)
        {
            Function = new FunctionDefinition(definition);
            this.precision = precision;

            SetDerivativeExpr(new Point("0"), 0);
        }
        public Derivative(FunctionDefinition definition, double precision = 0.00001)
        {
            Function = definition;
            this.precision = precision;

            SetDerivativeExpr(new Point("0"), 0);
        }

        public FunctionDefinition Function { get; set; }
        public double FirstDerivativeInPoint(Point x0, int variableIDX)
        {
            SetDerivativeExpr(x0, variableIDX);

            return _derivativeExpression.calculate();
        }
        public double SecondAverageRateOfChange(Point x0, double deltaX, int FirstArg, int SeconArg)
        {
            if (deltaX == 0)
            {
                throw new DivideByZeroException();
            }
            double fx0y0 = Function.GetValue(x0);
            x0.ChangePointValue(deltaX, FirstArg, OperationType.Add);
            double fx = Function.GetValue(x0);
            x0.ChangePointValue(deltaX, SeconArg, OperationType.Add);
            double f3x = Function.GetValue(x0);
            x0.ChangePointValue(-deltaX, FirstArg, OperationType.Add);
            double f2x = Function.GetValue(x0);

            double derivative = (f3x - f2x - fx + fx0y0) / (deltaX * deltaX);

            x0.ChangePointValue(-deltaX, SeconArg, OperationType.Add);
            return derivative;
        }
        public double SecondDerivativeInPoint(Point x0, int FirstArg, int SecondArg)
        {
            List<double> DifferenceRateOfChanges = new List<double>();
            double AverageRate_1 = 0;
            double AverageRate_2 = 0;

            AverageRate_1 = SecondAverageRateOfChange(x0, precision, FirstArg, SecondArg);
            AverageRate_2 = SecondAverageRateOfChange(x0, -precision, FirstArg, SecondArg);

            return (AverageRate_2 + AverageRate_1) / 2;
        }

        private void SetDerivativeExpr(Point point, int variableIDX)
        {
            string fun = Function.Function.getFunctionExpressionString();
            string DerVariable = Function.Function.getArgument(variableIDX).getArgumentName();

            var args = Function.Function.GetAllArguments();

            for (int i = 0; i < point.ListOfVariables.Length; i++)
            {
                args[i].setArgumentValue(point.ListOfVariables[i]);
            }
            if (_derivativeExpression == null)
                _derivativeExpression = new Expression($"der({fun},{DerVariable})");
            else
            {
                _derivativeExpression.removeAllArguments();
                _derivativeExpression.setExpressionString($"der({fun},{DerVariable})");
            }
            _derivativeExpression.addArguments(args);

        }
    }

}
