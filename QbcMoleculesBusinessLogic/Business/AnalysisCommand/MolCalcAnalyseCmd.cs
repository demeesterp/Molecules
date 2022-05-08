using QbcMoleculesBusinessLogic.Business.Logging;
using QbcMoleculesBusinessLogic.Data.CmdArgs.Analysis;
using QbcMoleculesBusinessLogic.Data.Molecules;
using QbcMoleculesBusinessLogic.Repo;
using QbcMoleculesBusinessLogic.Repo.Files;

namespace QbcMoleculesBusinessLogic.Business.AnalysisCommand
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

        public async Task<MolCalcAnalysisCmdResult> ProcessAsync(MolCalcAnalysisCmdInfo cmdInfo)
        {
            MolCalcAnalysisCmdResult retval = new MolCalcAnalysisCmdResult();

            foreach(var file in QbcFiles.FindFiles(cmdInfo.Path, "*.json"))
            {
                Molecule? result = MoleculeFileRepo.ReadFromFile(file);
                if ( result != null)
                {

                }
            }
            return await Task.FromResult(retval);
        }
    }
}
