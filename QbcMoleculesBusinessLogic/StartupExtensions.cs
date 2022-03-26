using Microsoft.Extensions.DependencyInjection;
using QbcMoleculesBusinessLogic.Business.Generator;
using QbcMoleculesBusinessLogic.Business.Logging;
using QbcMoleculesBusinessLogic.Business.Parser;
using QbcMoleculesBusinessLogic.Business.ProcessingCommand;
using QbcMoleculesBusinessLogic.Business.Processor;
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

            // Procerssors
            services.AddTransient<IMoleculesProcessor, MoleculesProcessor>();


            // Commands
            services.AddTransient<IMolDataCompilationCmd, MolDataCompilationCmd>();
            services.AddTransient<IMolCalcCoordCmd, MolCalcCoordCmd>();
            services.AddTransient<IMolCalcInitCmd, MolCalcInitCmd>();
            services.AddTransient<IMolCalcCmd, MolCalcCmd>();
            
            // Parsers
            services.AddTransient<IXyzParser, XyzParser>();
            services.AddTransient<IGordonGmsParser, GordonGmsParser>();

            // Generators
            services.AddTransient<IXyzGenerator, XyzGenerator>();
            services.AddTransient<IGmsInputGenerator, GmsInputGenerator>();



            // UserInteraction
            services.AddTransient<IUserInteractionService, UserInteractionService>();
           
        }

    }
}
