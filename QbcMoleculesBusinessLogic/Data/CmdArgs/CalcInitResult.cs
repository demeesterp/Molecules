using QbcMoleculesBusinessLogic.Data.Molecules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Data.CmdArgs
{
    public class CalcInitResult
    {
        public string BaseDir { get; set; }

        public List<Molecule> Result { get; set; } = new List<Molecule>();  

    }
}
