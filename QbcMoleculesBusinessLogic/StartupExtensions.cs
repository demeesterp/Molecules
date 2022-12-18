using Microsoft.Extensions.DependencyInjection;
using QbcMoleculesBusinessLogic.Applications.MoleculeCalculation;
using QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.Generator;
using QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.Parser;
using QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.ProcessingCommand;
using QbcMoleculesBusinessLogic.Applications.ResultAnalysis;
using QbcMoleculesBusinessLogic.Applications.ResultAnalysis.Services.AnalysisCommand;
using QbcMoleculesBusinessLogic.Business.Logging;
using QbcMoleculesBusinessLogic.Repo;
using QbcMoleculesBusinessLogic.Repo.Files;
using QbcMoleculesBusinessLogic.Repo.Formatter;
using QbcMoleculesBusinessLogic.Repo.Resources;
using QbcMoleculesBusinessLogic.UserInteraction;

namespace QbcMoleculesBusinessLogic
{
    public static class StartupExtensions
    {
        public static void AddQbcResearch(this IServiceCollection services)
        {
            // Helpers
            services.AddTransient<IQbcFormatter, QbcJsonFormatter>();
            services.AddTransient<IQbcFile, QbcFile>();
            services.AddTransient<IQbcResource, QbcResource>();
            services.AddTransient<IAtomInfoRepo, AtomInfoRepo>();
            services.AddTransient<IBasissetInfoRepo, BasissetInfoRepo>();
            services.AddTransient<IQbcLogger, QbcLogger>();
            
            // Repositories
            services.AddTransient<IMoleculeFileRepo, MoleculeFileRepo>();

            // Commands
            services.AddTransient<IMolCalcCoordCmd, MolCalcCoordCmd>();
            services.AddTransient<IMolCalcInitCmd, MolCalcInitCmd>();
            services.AddTransient<IMolCalcCmd, MolCalcCmd>();
            services.AddTransient<IMolCalcAnalyseCmd, MolCalcAnalyseCmd>();
            
            // Parsers
            services.AddTransient<IXyzParser, XyzParser>();
            services.AddTransient<IGordonGmsParser, GordonGmsParser>();

            // Generators
            services.AddTransient<IXyzGenerator, XyzGenerator>();
            services.AddTransient<IGmsInputGenerator, GmsInputGenerator>();

            // UserInteraction
            services.AddTransient<IUserInteractionService, UserInteractionService>();


            // Applications
            services.AddSingleton<ResultAnalyseApplication>();
            services.AddSingleton<MoleculeCalculationApplication>();
        }

    }
}
