using QbcMoleculesBusinessLogic.Data.CmdArgs;
using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Business.ProcessingCommand
{
    public class MolCalcCoordCmd : IMolCalcCoordCmd
    {
        public Task<CalcCoordResult> ProcessAsync(CalcCoordInfo info, Molecule molecule)
        {
            throw new NotImplementedException();
        }
    }
}
