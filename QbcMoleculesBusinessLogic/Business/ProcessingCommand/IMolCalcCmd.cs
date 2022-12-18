using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Business.ProcessingCommand
{
    public interface IMolCalcCmd
    {
        Task<List<Molecule>> ProcessAsync(string cmdInfo);
    }
}
