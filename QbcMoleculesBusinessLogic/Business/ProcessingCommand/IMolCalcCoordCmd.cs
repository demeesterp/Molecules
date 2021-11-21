using QbcMoleculesBusinessLogic.Data.CmdArgs;
using QbcMoleculesBusinessLogic.Data.Molecules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Business.ProcessingCommand
{
    public interface IMolCalcCoordCmd
    {

        Task<CalcCoordResult> ProcessAsync(CalcCoordInfo info, Molecule molecule);

    }
}
