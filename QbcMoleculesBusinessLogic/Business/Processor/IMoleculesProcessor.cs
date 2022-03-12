using QbcMoleculesBusinessLogic.Data.ProcessCommands;

namespace QbcMoleculesBusinessLogic.Business.Processor
{
    public interface IMoleculesProcessor
    {
        Task ProcessAsync(QbcCmd qbcCmd);
    }
}
