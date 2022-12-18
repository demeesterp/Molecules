using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Business.ProcessingCommand
{
    public interface IMolCalcCoordCmd
    {

        Task<Molecule> ProcessAsync(string info);

    }
}
