namespace QbcMoleculesBusinessLogic.Business.MoleculeMath
{
    public class DeterminantDouble
    {

        #region private members

        private double[,] Matrix
        {
            get;
        }

        #endregion


        public DeterminantDouble(double[,] data)
        {
            this.Matrix = data;
        }


        public double Value
        {
            get
            {
                return CalcDeterminant(this.Matrix);
            }
        }

        private double CalcDeterminant(double[,] input)
        {
            int order = int.Parse(System.Math.Sqrt(input.Length).ToString());
            if (order > 2)
            {
                double value = default;
                for (int j = 0; j < order; j++)
                {
                    double[,] Temp = CreateSmallerMatrix(input, 0, j);
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


        private double[,] CreateSmallerMatrix(double[,] input, int i, int j)
        {
            int order = int.Parse(System.Math.Sqrt(input.Length).ToString());
            double[,] output = new double[order - 1, order - 1];
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
