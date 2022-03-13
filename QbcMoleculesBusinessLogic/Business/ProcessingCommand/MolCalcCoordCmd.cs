using QbcMoleculesBusinessLogic.Business.Logging;
using QbcMoleculesBusinessLogic.Data.CmdArgs;
using QbcMoleculesBusinessLogic.Data.Molecules;
using QbcMoleculesBusinessLogic.Repo;
using QbcMoleculesBusinessLogic.Repo.Files;

namespace QbcMoleculesBusinessLogic.Business.ProcessingCommand
{
    public class MolCalcCoordCmd : IMolCalcCoordCmd
    {

        #region dependencies

        private IQbcLogger          Logger { get; }

        private IQbcFile            QbcFile  { get; }

        private IMoleculeFileRepo   MoleculeFileRepo { get; }

        #endregion


        public MolCalcCoordCmd(IQbcFile qbcFile, IMoleculeFileRepo moleculeFileRepo, IQbcLogger logger)
        {
            QbcFile = qbcFile;
            Logger = logger;
            MoleculeFileRepo = moleculeFileRepo;
        }



        public Task<CalcCoordResult> ProcessAsync(CalcCoordInfo info, Molecule molecule)
        {
            CalcCoordResult retval = new();
            foreach(string moleculeDir in QbcFile.FindDirectories(info.BasePath,"*"))
            {

            
            
            
            
            };
            return Task.FromResult(retval);
        }
    }
}
