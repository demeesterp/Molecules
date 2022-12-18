using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.ProcessingCommand
{
    public interface IMolCalcInitCmd
    {

        Task<List<Molecule>> ProcessAsync(string basePath);

    }
}
