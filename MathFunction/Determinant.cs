using System;

namespace MathFunction
{
    public static class Determinant
    {
        private static double epsilon { get; set; } = 0.00001;
        public static void SetEpsilon(double eps)
        {
            epsilon = eps;
        }
        public static double Det(double[,] matrix)
        {            
            if (matrix.Length == 1)
                return matrix[0, 0];
            if (!IsCorrectDim(matrix.Length) )
                throw new ArgumentException();

            switch (Math.Sqrt(matrix.Length))
            {
                case 2:
                    return Determinant2x2(matrix);
                case 3:
                    return Determinant3x3(matrix);
                default:
                    return Matrix.MatrixDeterminant(matrix);
            }
        }
        public static double Determinant2x2(double[,] matrix)
        {
            if (!IsCorrectDim(matrix.Length))
                throw new ArgumentException();
            return (matrix[0,0] * matrix[1,1]) - (matrix[0, 1] * matrix[1, 0]);
        }
        public static double Determinant3x3(double[,] matrix)
        {

            if (!IsCorrectDim(matrix.Length))
                throw new ArgumentException();
            return matrix[0, 0] * matrix[1, 1] * matrix[2, 2]
                + matrix[0, 1] * matrix[1, 2] * matrix[2, 0]
                + matrix[0, 2] * matrix[1, 0] * matrix[2, 1]
                    - (matrix[0, 2] * matrix[1, 1] * matrix[2, 0]
                + matrix[0, 0] * matrix[1, 2] * matrix[2, 1]
                + matrix[0, 1] * matrix[1, 0] * matrix[2, 2]);
        }
        private static bool IsCorrectDim(double matrixLength) => Math.Pow(Math.Sqrt(matrixLength), 2) == matrixLength;

    }
}
