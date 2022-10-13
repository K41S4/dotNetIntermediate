using MultiThreading.Task3.MatrixMultiplier.Matrices;
using System.Threading.Tasks;

namespace MultiThreading.Task3.MatrixMultiplier.Multipliers
{
    public class MatricesMultiplierParallel : IMatricesMultiplier
    {
        public IMatrix Multiply(IMatrix m1, IMatrix m2)
        {
            var m1Rows = m1.RowCount; var m1Cols = m1.ColCount;
            var m2Rows = m2.RowCount; var m2Cols = m2.ColCount;

            Matrix result = new Matrix(m1Rows, m2Cols);

            Parallel.For(0, m1Rows, i =>
            {
                for (int j = 0; j < m2Cols; ++j)
                { 
                    long res = 0;
                    for (int k = 0; k < m1Cols; ++k)
                    {
                        res += m1.GetElement(i, k) * m2.GetElement(k, j);
                    }
                    result.SetElement(i, j, res);
                }
            });

            return result;
        }
    }
}
