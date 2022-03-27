namespace QbcMoleculesBusinessLogic.Business.Parser
{
    public class LewisAcidPopulationAnalysisParser : UHFPopulationAnalysisParser
    {
        protected override PopulationAnalysisType GetPopulationStatus()
        {
            return PopulationAnalysisType.lewisacid;
        }
    }
}
