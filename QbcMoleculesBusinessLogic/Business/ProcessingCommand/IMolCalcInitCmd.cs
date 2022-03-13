using QbcMoleculesBusinessLogic.Data.CmdArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Business.ProcessingCommand
{
    public interface IMolCalcInitCmd
    {

        Task<CalcInitResult> ProcessAsync(CalcInitInfo initInfo);

    }
}
