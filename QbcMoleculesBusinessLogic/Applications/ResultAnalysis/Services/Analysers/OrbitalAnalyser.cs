using QbcMoleculesBusinessLogic.Data.AnalyseResult.Orbital;
using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Applications.ResultAnalysis.Services.Analysers
{
    public class OrbitalAnalyser
    {
        public static OrbitalAnalyseResult Analyse(Molecule molecule)
        {
            OrbitalAnalyseResult retval = new OrbitalAnalyseResult();
            List<OrbitalAnalysisItemAtom> shuttle = new List<OrbitalAnalysisItemAtom>();
            foreach(var atom in molecule.Atoms)
            {
                foreach(var orbital in atom.Orbitals)
                {
                    shuttle.Add(new OrbitalAnalysisItemAtom(atom, orbital));
                }
            }

            retval.Acid = shuttle.OrderByDescending(i => i.Orbital.MullikenPopulationAcid.GetValueOrDefault()).ToList();
            retval.Base = shuttle.OrderByDescending(i => i.Orbital.MullikenPopulationBase.GetValueOrDefault()).ToList();

           
            
            return retval;
        }

    }
}
