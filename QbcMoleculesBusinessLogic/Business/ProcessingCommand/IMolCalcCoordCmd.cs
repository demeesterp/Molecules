using QbcMoleculesBusinessLogic.Data.CmdArgs.Processing;

namespace QbcMoleculesBusinessLogic.Business.ProcessingCommand
{
    public interface IMolCalcCoordCmd
    {

        Task<CalcCoordResult> ProcessAsync(CalcCoordInfo info);

    }
}
