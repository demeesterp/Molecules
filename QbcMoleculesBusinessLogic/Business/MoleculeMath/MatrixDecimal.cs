namespace QbcMoleculesBusinessLogic.Business.MoleculeMath
{
    public class MatrixDecimal
    {

        #region private datamembers

        private decimal[,] Data { get; }

        #endregion

        public MatrixDecimal(uint rows, uint columns)
        {
            Data = new decimal[rows, columns];
            Rows = rows;
            Cols = columns;
        }

        public uint Rows { get; }

        public uint Cols { get; }

        public decimal Get(uint row, uint col)
        {
            return Data[row, col];
        }

        public void Set(uint row, uint col, decimal value)
        {
            Data[row, col] = value;
        }

        public decimal[] GetRow(uint row)
        {
            decimal[] retval = new decimal[Cols];
            for (uint col = 0; col < Cols; ++col)
            {
                retval[col] = Get(row, col);
            }
            return retval;
        }

        public decimal[] GetCol(uint col)
        {
            decimal[] retval = new decimal[Rows];
            for (uint row = 0; row < Rows; ++row)
            {
                retval[row] = Get(row, col);
            }
            return retval;
        }

        public static MatrixDecimal operator +(MatrixDecimal lhs, MatrixDecimal rhs)
        {
            if (lhs.Rows != rhs.Rows || lhs.Cols != rhs.Cols)
            {
                throw new NotSupportedException("Incompatible matrixes");
            }

            /*  */
            MatrixDecimal retval = new MatrixDecimal(lhs.Rows, lhs.Cols);
            for (UInt32 row = 0; row < retval.Rows; ++row)
            {
                for (UInt32 col = 0; col < retval.Cols; ++col)
                {
                    retval.Set(row, col, lhs.Get(row, col) + rhs.Get(row, col));
                }
            }
            return retval;
        }

        public static MatrixDecimal operator -(MatrixDecimal lhs, MatrixDecimal rhs)
        {
            if (lhs.Rows != rhs.Rows || lhs.Cols != rhs.Cols)
            {
                throw new NotSupportedException("Incompatible matrixes");
            }

            /*  */
            MatrixDecimal retval = new MatrixDecimal(lhs.Rows, lhs.Cols);
            for (UInt32 row = 0; row < retval.Rows; ++row)
            {
                for (UInt32 col = 0; col < retval.Cols; ++col)
                {
                    retval.Set(row, col, lhs.Get(row, col) - rhs.Get(row, col));
                }
            }
            return retval;
        }

        public static MatrixDecimal operator *(MatrixDecimal lhs, MatrixDecimal rhs)
        {
            if (lhs.Cols != rhs.Rows)
            {
                throw new NotSupportedException("Incompatible matrixes");
            }

            /*  */
            MatrixDecimal retval = new MatrixDecimal(lhs.Rows, rhs.Cols);
            for (uint row = 0; row < retval.Rows; ++row)
            {
                for (uint col = 0; col < retval.Cols; ++col)
                {
                    decimal sum = decimal.Zero;
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
