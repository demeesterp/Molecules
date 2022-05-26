namespace QbcMoleculesBusinessLogic.Business.MoleculeMath
{
    internal class MoleculeSolveEquation
    {
        public class EquationDouble
        {

            #region private datamembers

            private MatrixDouble Data { get; }

            private long Dimension { get; }

            #endregion

            public EquationDouble(long dimension)
            {
                Dimension = dimension;
                Data = new MatrixDouble(dimension, dimension + 1);
            }


            public void SetCoefficient(long row, long col, double value)
            {
                if (row < Dimension && col < Dimension)
                {
                    Data.Set(row, col, value);
                }
                else
                {
                    throw new NotSupportedException($"row {row} and or col {col} are out of range! Dimension {Dimension}");
                }

            }

            public void SetResult(long row, double value)
            {
                if (row < Dimension)
                {
                    Data.Set(row, Dimension, value);
                }
                else
                {
                    throw new NotSupportedException($"row {row} out of range! Dimension {Dimension}");
                }
            }


            public double[] Solve()
            {
                double[] retval = new double[Dimension];
                for (long k = 0; k < Dimension - 1; ++k)
                {
                    for (long i = k + 1; i < Dimension; ++i)
                    {
                        double qt = this.Data.Get(i, k) / this.Data.Get(k, k);
                        for (long j = k + 1; j < Dimension + 1; ++j)
                        {
                            double newvalue = this.Data.Get(i, j) - qt * this.Data.Get(k, j);
                            this.Data.Set(i, j, newvalue);
                        }
                    }
                }

                for (long i = Dimension - 1; i >= 0; --i)
                {
                    for (long j = i + 1; j < Dimension; ++j)
                    {
                        double result = this.Data.Get(i, Dimension) - this.Data.Get(i, j) * this.Data.Get(j, Dimension);
                        this.Data.Set(i, Dimension, result);
                    }

                    var outres = this.Data.Get(i, Dimension) / this.Data.Get(i, i);
                    this.Data.Set(i, Dimension, outres);
                }

                for (int row = 0; row < Dimension; ++row)
                {
                    retval[row] = this.Data.Get(row, Dimension);
                }

                return retval;
            }


        }
    }
}
