using QbcMoleculesBusinessLogic.Applications.Data;
using QbcMoleculesBusinessLogic.Business.ProcessingCommand;
using QbcMoleculesBusinessLogic.Data.CmdArgs.Processing;

namespace QbcMoleculesBusinessLogic.Applications
{
    public class MoleculeCalculationApplication : Application
    {
        #region

        private IMolCalcCmd MolCalcCmd { get; }

        #endregion

        public MoleculeCalculationApplication(IMolCalcCmd molCalcCmd)
        {
            MolCalcCmd = molCalcCmd;
        }

        public override string Name()
        {
            return nameof(MoleculeCalculationApplication);
        }

        public override async Task RunAsync(ApplicationParameters parameters)
        {
            Console.WriteLine($"Currently running {this.Name()} with parameters {parameters}");
            var param = parameters.Find("path");
            if ( param != null )
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
            Console.WriteLine("Press any key to stop the application");
            Console.ReadLine();
        }
    }
}
