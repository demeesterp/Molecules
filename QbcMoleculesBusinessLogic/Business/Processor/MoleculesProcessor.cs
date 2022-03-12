using QbcMoleculesBusinessLogic.Business.Logging;
using QbcMoleculesBusinessLogic.Data.ProcessCommands;

namespace QbcMoleculesBusinessLogic.Business.Processor
{
    public class MoleculesProcessor : IMoleculesProcessor
    {

        #region dependencies

        private IQbcLogger Logger { get; }

        #endregion

        public MoleculesProcessor(IQbcLogger logger)
        {
            Logger = logger;
        }


        public async Task ProcessAsync(QbcCmd qbcCmd)
        {
            switch(qbcCmd.Name)
            {
                case QbcCmdName.dummy:
                    Logger.LogWarning($"Command {qbcCmd.Name}");
                    await Task.CompletedTask;
                    break;
                case QbcCmdName.procescalculation:
                    Logger.LogWarning($"Command {qbcCmd.Name}");
                    await Task.CompletedTask;
                    break;
                default:
                    Logger.LogWarning($"Command {qbcCmd.Name} not yet implemented");
                    break;
            }
        }

    }
}
