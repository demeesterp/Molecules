using Microsoft.Extensions.DependencyInjection;
using QbcMoleculesBusinessLogic.Business.Generator;
using QbcMoleculesBusinessLogic.Business.Parser;
using QbcMoleculesBusinessLogic.Business.ProcessingCommand;
using QbcMoleculesBusinessLogic.Repo;
using QbcMoleculesBusinessLogic.Repo.Files;
using QbcMoleculesBusinessLogic.Repo.Formatter;
using QbcMoleculesBusinessLogic.Repo.Resources;

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
            
            
            // Repositories
            services.AddTransient<IMoleculeFileRepo, MoleculeFileRepo>();

            // Commands
            services.AddTransient<IMoleculeDataCollectionCmd, MoleculeDataCollectionCmd>();
            services.AddTransient<IMoleculeCalculationCoordinationCmd, MoleculeCalculationCoordinationCmd>();
            
            // Parsers and generators
            services.AddTransient<IXyzParser, XyzParser>();
            services.AddTransient<IGordonGmsParser, GordonGmsParser>();

            // Generators
            services.AddTransient<IXyzGenerator, XyzGenerator>();
           
        }

    }
}
