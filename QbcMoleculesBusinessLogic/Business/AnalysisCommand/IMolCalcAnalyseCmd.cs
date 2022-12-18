namespace QbcMoleculesBusinessLogic.Business.AnalysisCommand
{
    public interface IMolCalcAnalyseCmd
    {
        Task ProcessAsync(string cmdInfo);
    }
}
