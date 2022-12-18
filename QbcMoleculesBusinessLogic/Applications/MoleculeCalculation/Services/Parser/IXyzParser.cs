using QbcMoleculesBusinessLogic.Data.Molecules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.Parser
{
    public interface IXyzParser
    {
        Molecule? Parse(string xyzData);
    }
}
