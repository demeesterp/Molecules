namespace QbcMoleculesBusinessLogic.Business.MoleculeMath
{
    public class DeterminantDecimal
    {

        #region private members

        private decimal[,] Matrix
        {
            get;
        }

        #endregion


        public DeterminantDecimal(decimal[,] data)
        {
            this.Matrix = data;
        }


        public decimal Value
        {
            get
            {
                return CalcDeterminant(this.Matrix);
            }
        }

        private decimal CalcDeterminant(decimal[,] input)
        {
            int order = int.Parse(System.Math.Sqrt(input.Length).ToString());
            if (order > 2)
            {
                decimal value = default;
                for (int j = 0; j < order; j++)
                {
                    decimal[,] Temp = CreateSmallerMatrix(input, 0, j);
                    value = value + input[0, j] * (SignOfElement(0, j) * CalcDeterminant(Temp));
                }
                return value;
            }
            else if (order == 2)
            {
                return ((input[0, 0] * input[1, 1]) - (input[1, 0] * input[0, 1]));
            }
            else
            {
                return (input[0, 0]);
            }
        }


        private decimal[,] CreateSmallerMatrix(decimal[,] input, int i, int j)
        {
            int order = int.Parse(System.Math.Sqrt(input.Length).ToString());
            decimal[,] output = new decimal[order - 1, order - 1];
            int x = 0, y = 0;
            for (int m = 0; m < order; m++, x++)
            {
                if (m != i)
                {
                    y = 0;
                    for (int n = 0; n < order; n++)
                    {
                        if (n != j)
                        {
                            output[x, y] = input[m, n];
                            y++;
                        }
                    }
                }
                else
                {
                    x--;
                }
            }
            return output;
        }

        private int SignOfElement(int i, int j)
        {
            if ((i + j) % 2 == 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }


    }
}
