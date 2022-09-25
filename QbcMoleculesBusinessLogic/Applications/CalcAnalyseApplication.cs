using QbcMoleculesBusinessLogic.Applications.Data;
using QbcMoleculesBusinessLogic.Business.AnalysisCommand;
using QbcMoleculesBusinessLogic.Data.CmdArgs.Analysis;

namespace QbcMoleculesBusinessLogic.Applications
{
    public class CalcAnalyseApplication : Application
    {
        #region dependencies

        private IMolCalcAnalyseCmd MolCalcAnalyseCmd { get; }

        #endregion


        public CalcAnalyseApplication(IMolCalcAnalyseCmd molCalcAnalyseCmd)
        {
            MolCalcAnalyseCmd = molCalcAnalyseCmd;
        }


        public override string Name()
        {
            return nameof(CalcAnalyseApplication);
        }

        public override async Task RunAsync(ApplicationParameters parameters)
        {
            Console.WriteLine($"Currently running {this.Name()} with parameters {parameters}");
            var parameter = parameters.Find("path");
            if ( parameter != null)
            {
                await MolCalcAnalyseCmd.ProcessAsync(new MolCalcAnalysisCmdInfo()
                {
                    Path = parameter.Value
                });
            }
            else
            {
                await MolCalcAnalyseCmd.ProcessAsync(new MolCalcAnalysisCmdInfo()
                {
                    Path = Environment.CurrentDirectory
                });
            }
            Console.WriteLine("Press any key to stop the application");
            Console.ReadLine();
        }
    }
}
