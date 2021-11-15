using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Data.DataFiles
{
    public class Atom
    {

        public int ID { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string AtomNumber { get; set; } = string.Empty;

        public string AtomMass { get; set; } = string.Empty;

        public string AtomRadius { get; set; } = string.Empty;

        public string ElectroNegativity { get; set; } = string.Empty;

        public string ElectronAffinity { get; set; } = string.Empty;
    }
}
