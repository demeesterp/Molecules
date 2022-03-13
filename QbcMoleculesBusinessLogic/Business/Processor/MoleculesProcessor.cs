using QbcMoleculesBusinessLogic.Business.Logging;
using QbcMoleculesBusinessLogic.Business.ProcessingCommand;
using QbcMoleculesBusinessLogic.Data.CmdArgs;
using QbcMoleculesBusinessLogic.Data.ProcessCommands;

namespace QbcMoleculesBusinessLogic.Business.Processor
{
    public class MoleculesProcessor : IMoleculesProcessor
    {

        #region dependencies

        private IQbcLogger Logger { get; }

        private IMolCalcCmd MolCalcCmd { get; }

        #endregion

        public MoleculesProcessor(IMolCalcCmd molCalcCmd, IQbcLogger logger)
        {
            Logger = logger;
            MolCalcCmd = molCalcCmd;
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
                    var param = qbcCmd.Parameters.Find(i => i.Name.ToLower() == "path");
                    if (param != null)
                    {
                        await this.MolCalcCmd.ProcessAsync(new MolCalcCmdInfo()
                        {
                            BasePath = param.Value
                        });
                    }
                    else
                    {
                        await this.MolCalcCmd.ProcessAsync(new MolCalcCmdInfo()
                        {
                            BasePath = Environment.CurrentDirectory
                        });
                    }                    
                    await Task.CompletedTask;
                    break;
                default:
                    Logger.LogWarning($"Command {qbcCmd.Name} not yet implemented");
                    break;
            }
        }

    }
}
