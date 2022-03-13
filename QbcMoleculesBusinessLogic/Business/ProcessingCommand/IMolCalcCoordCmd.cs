using QbcMoleculesBusinessLogic.Data.CmdArgs;

namespace QbcMoleculesBusinessLogic.Business.ProcessingCommand
{
    public interface IMolCalcCoordCmd
    {

        Task<CalcCoordResult> ProcessAsync(CalcCoordInfo info);

    }
}
