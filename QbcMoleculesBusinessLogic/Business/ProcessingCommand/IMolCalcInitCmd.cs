using QbcMoleculesBusinessLogic.Data.CmdArgs.Processing;

namespace QbcMoleculesBusinessLogic.Business.ProcessingCommand
{
    public interface IMolCalcInitCmd
    {

        Task<CalcInitResult> ProcessAsync(CalcInitInfo initInfo);

    }
}
