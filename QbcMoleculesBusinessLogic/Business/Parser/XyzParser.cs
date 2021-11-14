using QbcMoleculesBusinessLogic.Business.Conversion;
using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Business.Parser
{
    public class XyzParser : IXyzParser
    {
        public Molecule? Parse(string xyzData)
        {
            Molecule? retval = null;
            if (!String.IsNullOrWhiteSpace(xyzData))
            {
                retval = new Molecule();   
                foreach (var line in xyzData.Split(new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
                {
                    var segments = line.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                    if (segments.Length >= 4)
                    {
                        retval.Atoms.Add(new MoleculeAtom()
                        {
                            Symbol = segments[0],
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
