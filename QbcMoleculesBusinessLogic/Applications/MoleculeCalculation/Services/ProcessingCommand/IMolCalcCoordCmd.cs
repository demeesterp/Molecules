using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.ProcessingCommand
{
    public interface IMolCalcCoordCmd
    {

        Task<Molecule> ProcessAsync(string basePath);

    }
}
