
namespace MathFunction
{
    using org.mariuszgromada.math.mxparser;
    public class FunctionDefinition
    {
        public Function Function { get; set; }
        private Expression Expression { get; set; }
        public FunctionDefinition(string functionDefinition)
        {
            Function = new Function(functionDefinition);
            Expression = new Expression(Function);            
        }
        public void SetNewExpression(string expression)
        {
            Function = new Function(expression);
            Expression = new Expression(Function);
        }
        public void SetNewExpression(Function func)
        {
            Function = func;
            Expression = new Expression(Function);
        }
        public double GetValue(Point point)
        {
            return GetValue(point.ToString());
        }
        public double GetValue(string point)
        {
            Expression.setExpressionString(ExpressionString(point));
            return Expression.calculate();
        }
        public double GetValue (params double[] pointVariables)
        {
            var p = Point.PointToString(pointVariables);
            return GetValue(p);
        }
        private string ExpressionString(string pointVariable)
        {
            return $"f({pointVariable})";
        }
    }
}
