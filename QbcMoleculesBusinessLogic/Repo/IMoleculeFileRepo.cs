using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Repo
{
    public interface IMoleculeFileRepo
    {
        Molecule? ReadFromFile(string baseDir, string moleculeName, string basisSetCode);
        
        Molecule? ReadFromFile(string fileName);


        void WriteToFile(Molecule? molecule, string baseDir);

        bool MoleculeExists(Molecule? molecule, string baseDir);

    }
}
