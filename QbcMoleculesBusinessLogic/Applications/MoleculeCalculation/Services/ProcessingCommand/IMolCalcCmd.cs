using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.ProcessingCommand
{
    public interface IMolCalcCmd
    {
        Task<List<Molecule>> ProcessAsync(string basePath);
    }
}
