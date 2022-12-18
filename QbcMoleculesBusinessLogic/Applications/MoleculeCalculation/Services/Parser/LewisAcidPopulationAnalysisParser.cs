namespace QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.Parser
{
    public class LewisAcidPopulationAnalysisParser : UHFPopulationAnalysisParser
    {
        protected override PopulationAnalysisType GetPopulationStatus()
        {
            return PopulationAnalysisType.lewisacid;
        }
    }
}
