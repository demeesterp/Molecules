using QbcMoleculesBusinessLogic.Data.CmdArgs.Analysis;

namespace QbcMoleculesBusinessLogic.Business.AnalysisCommand
{
    public interface IMolCalcAnalyseCmd
    {
        Task<MolCalcAnalysisCmdResult> ProcessAsync(MolCalcAnalysisCmdInfo cmdInfo);
    }
}
