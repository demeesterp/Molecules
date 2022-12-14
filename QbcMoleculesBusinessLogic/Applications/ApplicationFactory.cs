using Microsoft.Extensions.DependencyInjection;
using QbcMoleculesBusinessLogic.Applications.MoleculeCalculation;
using QbcMoleculesBusinessLogic.Applications.ResultAnalysis;

namespace QbcMoleculesBusinessLogic.Applications
{
    public static class ApplicationFactory
    {

        public static Application Create(string applicationName = "")
        {
            var services = new ServiceCollection();
            services.AddQbcResearch();
            string application = applicationName.Replace("-", "");
            if ( String.IsNullOrEmpty(application))
            {
                return new DummyApplication();
            }
            else if (application == "analysecalculation")
            {
                var applicationService = services.BuildServiceProvider().GetService<ResultAnalyseApplication>();
                if (applicationService == null)
                {
                    throw new ArgumentNullException(applicationName);
                }
                return applicationService;
            }
            else if (application == "processcalculation")
            {
                var applicationService = services.BuildServiceProvider().GetService<MoleculeCalculationApplication>();
                if (applicationService == null)
                {
                    throw new ArgumentNullException(applicationName);
                }
                return applicationService;
            }
            else
            {
                return new DummyApplication();
            }


           
        }
    }
}
