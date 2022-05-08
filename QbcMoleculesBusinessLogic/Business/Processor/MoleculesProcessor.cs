using QbcMoleculesBusinessLogic.Business.AnalysisCommand;
using QbcMoleculesBusinessLogic.Business.Logging;
using QbcMoleculesBusinessLogic.Business.ProcessingCommand;
using QbcMoleculesBusinessLogic.Data.CmdArgs;
using QbcMoleculesBusinessLogic.Data.CmdArgs.Analysis;
using QbcMoleculesBusinessLogic.Data.CmdArgs.Processing;
using QbcMoleculesBusinessLogic.Data.ProcessCommands;

namespace QbcMoleculesBusinessLogic.Business.Processor
{
    public class MoleculesProcessor : IMoleculesProcessor
    {

        #region dependencies

        private IQbcLogger Logger { get; }

        private IMolCalcCmd MolCalcCmd { get; }

        private IMolCalcAnalyseCmd MolCalcAnalysisCmd { get; }

        #endregion

        public MoleculesProcessor(IMolCalcCmd molCalcCmd,
                                    IMolCalcAnalyseCmd molCalcAnalyseCmd,
                                            IQbcLogger logger)
        {
            Logger = logger;
            MolCalcCmd = molCalcCmd;
            MolCalcAnalysisCmd = molCalcAnalyseCmd;
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
                    break;
                case QbcCmdName.analysecalculation:
                    Logger.LogInformation($"Command {qbcCmd.Name}");
                    param = qbcCmd.Parameters.Find(i => i.Name.ToLower() == "path");
                    if (param != null)
                    {
                        await this.MolCalcAnalysisCmd.ProcessAsync(new MolCalcAnalysisCmdInfo()
                        {
                            Path = param.Value
                        });
                    }
                    else
                    {
                        await this.MolCalcCmd.ProcessAsync(new MolCalcCmdInfo()
                        {
                            BasePath = Environment.CurrentDirectory
                        });
                    }
                    break;
                default:
                    Logger.LogWarning($"Command {qbcCmd.Name} not yet implemented");
                    break;
            }
        }

    }
}
