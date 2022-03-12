using QbcMoleculesBusinessLogic.Business.Logging;
using QbcMoleculesBusinessLogic.Business.ProcessingCommand;
using QbcMoleculesBusinessLogic.Data.ProcessCommands;

namespace QbcMoleculesBusinessLogic.Business.Processor
{
    public class MoleculesProcessor : IMoleculesProcessor
    {

        #region dependencies

        private IQbcLogger Logger { get; }

        private IMolCalcInitCmd MolCalcInitCmd { get; }

        #endregion

        public MoleculesProcessor(IMolCalcInitCmd molCalcInitCmd, IQbcLogger logger)
        {
            MolCalcInitCmd = molCalcInitCmd;
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
                case QbcCmdName.processcalculation:
                    Logger.LogInformation($"Command {qbcCmd.Name}");
                    await Task.CompletedTask;
                    break;
                default:
                    Logger.LogWarning($"Command {qbcCmd.Name} not yet implemented");
                    break;
            }
        }

    }
}
