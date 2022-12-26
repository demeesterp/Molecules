using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Data.AnalyseResult.Orbital
{
    public class OrbitalAnalysisItemAtom
    {

        public OrbitalAnalysisItemAtom(MoleculeAtom item, MoleculeAtomOrbital orbital)
        {
            Atom = item;
            Orbital= orbital;
        }

        public MoleculeAtom Atom { get; }
    
        public MoleculeAtomOrbital Orbital { get; set; }
    }

}
