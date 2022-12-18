using QbcMoleculesBusinessLogic.Applications.ResultAnalysis.Services.AnalysisCommand;

namespace QbcMoleculesBusinessLogic.Applications.ResultAnalysis
{
    public class ResultAnalyseApplication : Application
    {
        #region dependencies

        private IMolCalcAnalyseCmd MolCalcAnalyseCmd { get; }

        #endregion


        public ResultAnalyseApplication(IMolCalcAnalyseCmd molCalcAnalyseCmd)
        {
            MolCalcAnalyseCmd = molCalcAnalyseCmd;
        }


        public override string Name()
        {
            return nameof(ResultAnalyseApplication);
        }

        public override async Task RunAsync(ApplicationParameters parameters)
        {
            Console.WriteLine($"Currently running {Name()} with parameters {parameters}");
            var parameter = parameters.Find("path");
            if (parameter != null)
            {
                await MolCalcAnalyseCmd.ProcessAsync(parameter.Value);
            }
            else
            {
                await MolCalcAnalyseCmd.ProcessAsync(Environment.CurrentDirectory);
            }
            Console.WriteLine("Press any key to stop the application");
            Console.ReadLine();
        }
    }
}
