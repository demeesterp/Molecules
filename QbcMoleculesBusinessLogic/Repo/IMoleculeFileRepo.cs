using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Repo
{
    public interface IMoleculeFileRepo
    {
        Molecule? ReadFromFile(string path);

        void WriteToFile(Molecule? molecule, string path);

        bool MoleculeExists(Molecule? molecule, string path);

    }
}
