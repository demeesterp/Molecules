using QbcMoleculesBusinessLogic.Business.Conversion;

namespace QbcMoleculesBusinessLogic.Business.Parser
{
    public abstract class FukuiEnergyParser
    {
        #region tags

        protected abstract string GetStartTag();

        protected abstract string GetEnergyTag();

        #endregion

        public Decimal Parse(List<string> input)
        {
            Decimal retval = Decimal.Zero;
            bool startProcessing = false;
            string line = string.Empty;
            for (int c = 0; c < input.Count; ++c)
            {
                line = input[c];

                if (line.Contains(GetStartTag()))
                {
                    startProcessing = true;
                }

                if (startProcessing && line.Contains(GetStartTag()))
                {
                    var results = line.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    if (results.Length == 2)
                    {
                        retval = QbcStringConvert.ToDecimal(results[1].Trim());
                    }
                }
            }
            return retval;
        }
    }
}
