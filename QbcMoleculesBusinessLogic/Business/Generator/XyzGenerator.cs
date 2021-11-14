using QbcMoleculesBusinessLogic.Data.Molecules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Business.Generator
{
    public class XyzGenerator : IXyzGenerator
    {
        public string GenerateXyzFile(Molecule molecule)
        {
           StringBuilder retval = new ();
            retval.AppendLine(molecule.Atoms.Count.ToString());
            retval.AppendLine();
            foreach(var atom in molecule.Atoms)
            {
                retval.AppendLine($"{atom.Symbol} {atom.PosX:0.00000} {atom.PosY:0.00000} {atom.PosZ:0.00000}");
            }
           return retval.ToString();
        }
    }
}
