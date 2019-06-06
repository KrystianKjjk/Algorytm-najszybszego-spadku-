using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MathFunction.Tests
{
    public class DeterminantTests
    {
        [Test]
        public void Determinant2x2tests()
        {
            var matrix = new double[2,2];
            var x = matrix.Rank;
            var y = matrix.Length;
            var z = matrix.SyncRoot;
            var a = matrix.IsFixedSize;
            var det = Determinant.Determinant2x2(matrix);
        }
        [Test]
        public void Determinanttests()
        {
            var matrix = new double[6, 6] 
            { 
                { 4,5,6,7,89,1 }, 
            { 6,6,5,4,3,1}, 
            {7,5,4,4,4,4 }, 
            { 56,524,4,4,4,4 }, 
            { 1,22,3,6,4,4 }, 
            { 6,7,1,6,7,2 }
            };

            var det = Determinant.Det(matrix);
            var expeected = -27541428;
            if (Math.Abs(det - expeected) < Math.Abs(expeected *0.02))
            {
                Assert.Pass();
            }
            else
            {
                Assert.AreEqual(expeected, det);
            }
        }
    }
}

