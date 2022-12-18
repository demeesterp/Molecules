using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Business.ProcessingCommand
{
    public interface IMolCalcInitCmd
    {

        Task<List<Molecule>> ProcessAsync(string initInfo);

    }
}
