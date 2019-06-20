using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MathFunction
{
    public class Point
    {
        private static readonly NumberFormatInfo info = new CultureInfo("en-US", false).NumberFormat;
        private string _stringVariables;
        private double[] _listOfVariables;

        public Point(string point)
        {
            StringVariables = point;
            FunctionValue = null;
        }
        public Point(params double[] variables)
        {
            ListOfVariables = variables;
            FunctionValue = null;
        }
        public double? FunctionValue { get; set; }
        public string StringVariables
        {
            get { return _stringVariables; }
            set
            {
                _stringVariables = value;
                _listOfVariables = PointToList(value);
            }
        }
        public double[] ListOfVariables
        {
            get { return _listOfVariables; }
            set
            {
                _listOfVariables = value;
                _stringVariables = PointToString(value);
            }
        }
        public static string PointToString(IEnumerable<double> point)
        {
            var stringPoint = "";
            foreach (var value in point)
            {
                stringPoint += value.ToString("G",info) + ",";
            }
            if(stringPoint.Length > 0 && stringPoint.Last()==',')
                stringPoint = stringPoint.Remove(stringPoint.Length - 1);
            return stringPoint;
        }
        public static double[] PointToList(string point)
        {
            var pointValues = point.Split(',');
            var variables = new double[pointValues.Count()];

            for (int i=0; i<pointValues.Count(); ++i)
            {
                if(double.TryParse(pointValues[i].Replace('.',','), out var result))
                {
                    variables[i] = result;
                }
                else
                {
                    throw new FormatException("not validated point");
                }
            }
            return variables;
        }
        public void ChangePointValue(double newValue, int NoOfVariable, OperationType value = OperationType.Replace)
        {
            switch (value)
            {
                case MathFunction.OperationType.Add:
                    ListOfVariables[NoOfVariable] += newValue;
                    break;
                case MathFunction.OperationType.Replace:
                    ListOfVariables[NoOfVariable] = newValue;
                    break;
                default:
                    throw new ArgumentException();
            }
            
            _stringVariables = PointToString(ListOfVariables);
        }
        public double GetVariable(int NoOfVariable)=> ListOfVariables[NoOfVariable];
        public override string ToString() =>StringVariables;
        public static bool operator== (Point arg1, Point arg2)
        {
            if (arg1 is null && arg2 is null)
                return true;
            if (arg2 is null)
                return false;
            return arg1.StringVariables == arg2.StringVariables;
        }
        public static bool operator !=(Point arg1, Point arg2)
        {
            return !(arg1 == arg2);
        }
        public override bool Equals(object obj)
        {
            var point = obj as Point;
            return point != null &&
                   _stringVariables == point._stringVariables &&
                   EqualityComparer<double[]>.Default.Equals(_listOfVariables, point._listOfVariables) &&
                   StringVariables == point.StringVariables &&
                   EqualityComparer<double[]>.Default.Equals(ListOfVariables, point.ListOfVariables);
        }
        public override int GetHashCode()
        {
            var hashCode = -1399997414;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_stringVariables);
            hashCode = hashCode * -1521134295 + EqualityComparer<double[]>.Default.GetHashCode(_listOfVariables);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(StringVariables);
            hashCode = hashCode * -1521134295 + EqualityComparer<double[]>.Default.GetHashCode(ListOfVariables);
            return hashCode;
        }
        public static IEnumerable<double> PointsDifference(Point A, Point B)
        {
            if (A.ListOfVariables.Length != B.ListOfVariables.Length)
                throw new IncorrectDimensionException();

            for (int i = 0; i < A.ListOfVariables.Length; i++)
                yield return A.ListOfVariables[i] - B.ListOfVariables[i];
        }
    }
    public enum OperationType
    {
        Add, 
        Replace
    }
}
