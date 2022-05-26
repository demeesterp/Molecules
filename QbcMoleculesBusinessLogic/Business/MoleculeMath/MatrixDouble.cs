namespace QbcMoleculesBusinessLogic.Business.MoleculeMath
{
    public class MatrixDouble
    {

        #region private datamembers

        private double[,] Data { get; }

        #endregion

        public MatrixDouble(long rows, long columns)
        {
            if (rows > 0 && columns > 0)
            {
                Data = new double[rows, columns];
                Rows = rows;
                Cols = columns;
            }
            else
            {
                throw new NotSupportedException("row and column must be larger than 0!");
            }
        }

        public long Rows { get; }

        public long Cols { get; }

        public double Get(long row, long col)
        {
            return Data[row, col];
        }

        public void Set(long row, long col, double value)
        {
            Data[row, col] = value;
        }

        public double[] GetRow(long row)
        {
            double[] retval = new double[Cols];
            for (uint col = 0; col < Cols; ++col)
            {
                retval[col] = Get(row, col);
            }
            return retval;
        }

        public double[] GetCol(long col)
        {
            double[] retval = new double[Rows];
            for (uint row = 0; row < Rows; ++row)
            {
                retval[row] = Get(row, col);
            }
            return retval;
        }

        public static MatrixDouble operator +(MatrixDouble lhs, MatrixDouble rhs)
        {
            if (lhs.Rows != rhs.Rows || lhs.Cols != rhs.Cols)
            {
                throw new NotSupportedException("Incompatible matrixes");
            }

            /*  */
            MatrixDouble retval = new (lhs.Rows, lhs.Cols);
            for (UInt32 row = 0; row < retval.Rows; ++row)
            {
                for (UInt32 col = 0; col < retval.Cols; ++col)
                {
                    retval.Set(row, col, lhs.Get(row, col) + rhs.Get(row, col));
                }
            }
            return retval;
        }

        public static MatrixDouble operator -(MatrixDouble lhs, MatrixDouble rhs)
        {
            if (lhs.Rows != rhs.Rows || lhs.Cols != rhs.Cols)
            {
                throw new NotSupportedException("Incompatible matrixes");
            }

            /*  */
            MatrixDouble retval = new (lhs.Rows, lhs.Cols);
            for (UInt32 row = 0; row < retval.Rows; ++row)
            {
                for (UInt32 col = 0; col < retval.Cols; ++col)
                {
                    retval.Set(row, col, lhs.Get(row, col) - rhs.Get(row, col));
                }
            }
            return retval;
        }

        public static MatrixDouble operator *(MatrixDouble lhs, MatrixDouble rhs)
        {
            if (lhs.Cols != rhs.Rows)
            {
                throw new NotSupportedException("Incompatible matrixes");
            }

            /*  */
            MatrixDouble retval = new (lhs.Rows, rhs.Cols);
            for (uint row = 0; row < retval.Rows; ++row)
            {
                for (uint col = 0; col < retval.Cols; ++col)
                {
                    double sum = 0;
                    for (uint i = 0; i < lhs.Cols; ++i)
                    {
                        sum += lhs.Get(row, col) * rhs.Get(i, col);
                    }
                    retval.Set(row, col, sum);
                }
            }
            return retval;
        }

    }
}
