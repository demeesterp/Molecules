using QbcMoleculesBusinessLogic.Business.Conversion;
using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.Parser
{
    public static class GordonGmsParseUtilities
    {

        public static MoleculeAtom ParseOptAtomPosition(string line)
        {
            MoleculeAtom retval = new MoleculeAtom();

            var current = FindNthSegment(line, 1);
            string atomsymbol = line.Substring(current.Item1, current.Item2 - current.Item1);
            current = FindNthSegment(line, 2);
            string charge = line.Substring(current.Item1, current.Item2 - current.Item1);

            string posx = line.Substring(current.Item2, 15);
            string posy = line.Substring(current.Item2 + 15, 15);
            string posz = line.Substring(current.Item2 + 30, 15);

            retval.Symbol = atomsymbol;
            retval.AtomicWeight = (int)QbcStringConvert.ToDecimal(charge);
            retval.PosX = QbcStringConvert.ToDecimal(posx);
            retval.PosY = QbcStringConvert.ToDecimal(posy);
            retval.PosZ = QbcStringConvert.ToDecimal(posz);

            return retval;
        }

        public static Tuple<int, int> FindNthSegment(string input, int n)
        {
            int currentbegin = 0;
            int currentend = 0;
            while (n > 0)
            {
                currentbegin = FindFirstNoSpace(input, currentend);
                currentend = FindFirstSpace(input, currentbegin);
                --n;
            }
            return new Tuple<int, int>(currentbegin, currentend);
        }

        public static int FindFirstNoSpace(string input, int startPos = 0)
        {
            int retval = startPos;
            for (int c = startPos; c < input.Length; ++c)
            {
                if (input[c] != ' ')
                {
                    retval = c;
                    break;
                }
            }
            return retval;
        }

        public static int FindFirstSpace(string input, int startPos = 0)
        {
            int retval = startPos;
            for (int c = startPos; c < input.Length; ++c)
            {
                if (input[c] == ' ')
                {
                    retval = c;
                    break;
                }
            }
            return retval;
        }



    }
}
