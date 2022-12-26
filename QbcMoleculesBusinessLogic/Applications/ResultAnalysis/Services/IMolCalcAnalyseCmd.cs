namespace QbcMoleculesBusinessLogic.Applications.ResultAnalysis.Services.AnalysisCommand
{
    public interface IMolCalcAnalyseCmd
    {
        Task ProcessAsync(string cmdInfo);
    }
}
