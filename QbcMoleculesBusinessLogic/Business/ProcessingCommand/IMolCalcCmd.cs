using QbcMoleculesBusinessLogic.Data.CmdArgs;

namespace QbcMoleculesBusinessLogic.Business.ProcessingCommand
{
    public interface IMolCalcCmd
    {
        Task<MolCalcCmdResult> ProcessAsync(MolCalcCmdInfo cmdInfo);
    }
}
