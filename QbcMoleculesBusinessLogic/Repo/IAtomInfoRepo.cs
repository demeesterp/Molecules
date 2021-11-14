using QbcMoleculesBusinessLogic.Data.Molecules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Repo
{
    public interface IAtomInfoRepo
    {
        List<Atom> GetAtomInfo();

    }
}
