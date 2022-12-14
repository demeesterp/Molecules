using QbcMoleculesBusinessLogic.Business.Conversion;
using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.Parser
{
    public class XyzParser : IXyzParser
    {
        public Molecule? Parse(string xyzData)
        {
            Molecule? retval = null;
            if (!string.IsNullOrWhiteSpace(xyzData))
            {
                retval = new Molecule();
                int position = 1;
                foreach (var line in xyzData.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var segments = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (segments.Length >= 4)
                    {
                        retval.Atoms.Add(new MoleculeAtom()
                        {
                            Symbol = segments[0],
                            Position = position++,
                            Number = position - 1,
                            PosX = QbcStringConvert.ToDecimal(segments[1].Trim()),
                            PosY = QbcStringConvert.ToDecimal(segments[2].Trim()),
                            PosZ = QbcStringConvert.ToDecimal(segments[3].Trim())
                        });
                    }
                }
            }
            return retval;
        }
    }
}
