using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Data.AnalyseResult.Orbital
{
    public class OrbitalAnalyseResult
    {
        public List<OrbitalAnalysisItemAtom> Acid { get; set; }

        public List<OrbitalAnalysisItemAtom> Base { get; set; }


        public List<OrbitalAnalysisItemAtom> GetAcidDistribution(decimal treshHold)
        {
            decimal sum = 0.0M;
            return Acid.TakeWhile(item =>
            {
                sum += item.Orbital.MullikenPopulationAcid.GetValueOrDefault();
                return sum >= treshHold;
            }).ToList();
        }

        public List<OrbitalAnalysisItemAtom> GetBaseDistribution(decimal treshHold)
        {
            decimal sum = 0.0M;
            return Base.TakeWhile(item =>
            {
                sum += item.Orbital.MullikenPopulationBase.GetValueOrDefault();
                return sum >= treshHold;
            }).ToList();
        }

    }
}
