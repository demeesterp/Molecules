using QbcMoleculesBusinessLogic.Applications.ResultAnalysis.Services.Analysers;
using QbcMoleculesBusinessLogic.Applications.ResultAnalysis.Services.AnalysisCommand;
using QbcMoleculesBusinessLogic.Business.Logging;
using QbcMoleculesBusinessLogic.Data.Molecules;
using QbcMoleculesBusinessLogic.Repo;
using QbcMoleculesBusinessLogic.Repo.Files;

namespace QbcMoleculesBusinessLogic.Applications.ResultAnalysis.Services
{
    public class MolCalcAnalyseCmd : IMolCalcAnalyseCmd
    {
        #region private datamembers

        private IQbcLogger Logger { get; }

        private IQbcFile QbcFiles { get; }

        private IMoleculeFileRepo MoleculeFileRepo { get; }

        #endregion

        public MolCalcAnalyseCmd(IQbcFile qbcFile, IMoleculeFileRepo moleculeFileRepo, IQbcLogger logger)
        {
            Logger = logger;
            QbcFiles = qbcFile;
            MoleculeFileRepo = moleculeFileRepo;
        }

        public async Task ProcessAsync(string cmdInfo)
        {
            if (QbcFiles.PathExists(cmdInfo))
            {
                foreach (var file in QbcFiles.FindFiles(cmdInfo, "*.json"))
                {
                    Molecule? result = MoleculeFileRepo.ReadFromFile(file);
                    if (result != null)
                    {
                        OrbitalAnalyser.Analyse(result);
                    }
                }
            }
            else
            {
                Logger.LogError($"Path {cmdInfo} does not exists");
            }

            await Task.CompletedTask;
        }
    }
}
