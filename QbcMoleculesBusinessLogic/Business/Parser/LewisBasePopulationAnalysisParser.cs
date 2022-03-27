namespace QbcMoleculesBusinessLogic.Business.Parser
{
    public class LewisBasePopulationAnalysisParser : UHFPopulationAnalysisParser
    {
        protected override PopulationAnalysisType GetPopulationStatus()
        {
            return PopulationAnalysisType.lewisbase;
        }
    }
}
