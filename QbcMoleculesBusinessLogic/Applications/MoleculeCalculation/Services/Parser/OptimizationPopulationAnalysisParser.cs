using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.Parser
{
    public class OptimizationPopulationAnalysisParser : PopulationAnalysisParser
    {
        #region Tags

        private const string OptimizationResultTag = "***** EQUILIBRIUM GEOMETRY LOCATED *****";

        private const string StartTag = "          MULLIKEN AND LOWDIN POPULATION ANALYSES";

        private const string StartTagAOPopulations = "               ----- POPULATIONS IN EACH AO -----";

        private const string StartTagOverlapPopulations = "          ----- MULLIKEN ATOMIC OVERLAP POPULATIONS -----";

        private const string StartTagPopulations = "          TOTAL MULLIKEN AND LOWDIN ATOMIC POPULATIONS";

        private const string StartTagBondOrder = "          BOND ORDER AND VALENCE ANALYSIS";

        #endregion

        #region abstract overrided

        protected override string GetGeometryResultTag()
        {
            return OptimizationResultTag;
        }

        protected override string GetStartTag()
        {
            return StartTag;
        }

        protected override string GetStartTagAOPopulations()
        {
            return StartTagAOPopulations;
        }

        protected override string GetStartTagBondOrder()
        {
            return StartTagBondOrder;
        }

        protected override string GetStartTagOverlapPopulations()
        {
            return StartTagOverlapPopulations;
        }

        protected override string GetStartTagPopulations()
        {
            return StartTagPopulations;
        }

        protected override PopulationAnalysisType GetPopulationStatus()
        {
            return PopulationAnalysisType.neutral;
        }

        #endregion
    }
}
