using QbcMoleculesBusinessLogic.Data.Molecules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.Generator
{
    public interface IXyzGenerator
    {
        string GenerateXyzFile(Molecule molecule);

    }
}
