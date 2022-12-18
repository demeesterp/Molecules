using QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.ProcessingCommand;

namespace QbcMoleculesBusinessLogic.Applications.MoleculeCalculation
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
            Console.WriteLine($"Currently running {Name()} with parameters {parameters}");
            var param = parameters.Find("path");
            if (param != null)
            {
                await MolCalcCmd.ProcessAsync(param.Value);
            }
            else
            {
                await MolCalcCmd.ProcessAsync(Environment.CurrentDirectory);
            }
            Console.WriteLine("Press any key to stop the application");
            Console.ReadLine();
        }
    }
}
