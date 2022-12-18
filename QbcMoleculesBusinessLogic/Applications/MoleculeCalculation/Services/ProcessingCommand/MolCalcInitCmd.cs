using QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.Parser;
using QbcMoleculesBusinessLogic.Business.Logging;
using QbcMoleculesBusinessLogic.Data.Molecules;
using QbcMoleculesBusinessLogic.Repo;
using QbcMoleculesBusinessLogic.Repo.Files;

namespace QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.ProcessingCommand
{
    public class MolCalcInitCmd : IMolCalcInitCmd
    {

        #region dependencies

        private IQbcFile QbcFile { get; }

        private IXyzParser XyzParser { get; }

        private IQbcLogger QbcLogger { get; }

        private IMoleculeFileRepo MoleculeFileRepo { get; }

        #endregion

        public MolCalcInitCmd(IQbcFile qbcFile, IXyzParser xyzParser, IMoleculeFileRepo moleculeFileRepo, IQbcLogger logger)
        {
            QbcFile = qbcFile ?? throw new ArgumentNullException(nameof(qbcFile));
            QbcLogger = logger ?? throw new ArgumentNullException(nameof(logger));
            XyzParser = xyzParser ?? throw new ArgumentNullException(nameof(xyzParser));
            MoleculeFileRepo = moleculeFileRepo ?? throw new ArgumentNullException(nameof(moleculeFileRepo));
        }

        public async Task<List<Molecule>> ProcessAsync(string basePath)
        {
            QbcLogger.LogInformation($"MolCalcInit {basePath}");
            List<Molecule> molecules = new();
            foreach (string xyzFile in QbcFile.FindFiles(basePath, "*.xyz"))
            {
                string xyzdata = QbcFile.ReadText(xyzFile);
                Molecule? result = XyzParser.Parse(xyzdata);
                if (result != null)
                {
                    string[] nameInfo = Path.GetFileNameWithoutExtension(xyzFile).Split("_", StringSplitOptions.RemoveEmptyEntries);
                    result.NameInfo = nameInfo.First();
                    if (nameInfo.Length > 1)
                    {
                        result.Charge = int.TryParse(nameInfo[1], out int charge) ? charge : 0;
                    }
                    if (!MoleculeFileRepo.MoleculeExists(result, basePath))
                    {
                        MoleculeFileRepo.WriteToFile(result, basePath);
                    }
                    molecules.Add(result);
                }
            }
            return await Task.FromResult(molecules);
        }
    }
}
