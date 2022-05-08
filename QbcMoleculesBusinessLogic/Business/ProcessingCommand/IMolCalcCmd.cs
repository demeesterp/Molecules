using QbcMoleculesBusinessLogic.Data.CmdArgs.Processing;

namespace QbcMoleculesBusinessLogic.Business.ProcessingCommand
{
    public interface IMolCalcCmd
    {
        Task<MolCalcCmdResult> ProcessAsync(MolCalcCmdInfo cmdInfo);
    }
}
