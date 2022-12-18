namespace QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.Parser
{
    public class LewisBasePopulationAnalysisParser : UHFPopulationAnalysisParser
    {
        protected override PopulationAnalysisType GetPopulationStatus()
        {
            return PopulationAnalysisType.lewisbase;
        }
    }
}
