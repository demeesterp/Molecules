using QbcMoleculesBusinessLogic.Business.Generator;
using QbcMoleculesBusinessLogic.Business.Logging;
using QbcMoleculesBusinessLogic.Data.CmdArgs;
using QbcMoleculesBusinessLogic.Data.DataFiles;
using QbcMoleculesBusinessLogic.Data.Molecules;
using QbcMoleculesBusinessLogic.Repo;
using QbcMoleculesBusinessLogic.Repo.Files;
using QbcMoleculesBusinessLogic.UserInteraction;

namespace QbcMoleculesBusinessLogic.Business.ProcessingCommand
{
    public class MolCalcCoordCmd : IMolCalcCoordCmd
    {

        #region dependencies

        private IQbcLogger Logger { get; }

        private IQbcFile QbcFile { get; }

        private IMoleculeFileRepo MoleculeFileRepo { get; }

        private IUserInteractionService UserInteractionService { get; }

        private IGmsInputGenerator GmsInputGenerator { get; }

        #endregion


        public MolCalcCoordCmd(IQbcFile qbcFile,
                                IMoleculeFileRepo moleculeFileRepo,
                                IUserInteractionService userInteractionService,
                                IGmsInputGenerator gmsInputGenerator,
                                IQbcLogger logger)
        {
            QbcFile = qbcFile;
            Logger = logger;
            MoleculeFileRepo = moleculeFileRepo;
            UserInteractionService = userInteractionService;
            GmsInputGenerator = gmsInputGenerator;
        }



        public Task<CalcCoordResult> ProcessAsync(CalcCoordInfo info)
        {
            CalcCoordResult retval = new();
            BasisSet? basisset = this.UserInteractionService.SelectBasisSet();
            if (basisset != null)
            {
                foreach (string moleculeFile in QbcFile.FindFiles(info.BasePath, "*.json"))
                {
                    Molecule? mol = this.MoleculeFileRepo.ReadFromFile(moleculeFile);
                    if ( mol != null)
                    {
                        CreateGeoOptFile(info.BasePath, mol, basisset);

                    }
                    
                }
            }
            return Task.FromResult(retval);
        }


        private void CreateGeoOptFile(string directory, Molecule molecule, BasisSet basisSet)
        {
            string fileName = $"geoopt_{basisSet.Code}_{molecule.NameInfo}.inp";
            var inputFiles = QbcFile.FindFiles(directory, fileName);
            if (!inputFiles.Any())
            {
                QbcFile.WriteText(Path.Combine(directory, fileName),
                                GmsInputGenerator.GenGeoOptInput(molecule.Atoms,
                                                                    basisSet.Code,
                                                                        molecule.Charge.GetValueOrDefault()));
            }
        }


        private void CreateCHelpGChargeFile(string directory, Molecule molecule, BasisSet basisSet)
        {
            string fileName = $"chelpg_{basisSet.Code}_{molecule.NameInfo}.inp";
            var inputFiles = QbcFile.FindFiles(directory, fileName);
            if (!inputFiles.Any())
            {
                QbcFile.WriteText(Path.Combine(directory, fileName),
                                GmsInputGenerator.GenCHelpGChargeInput(molecule.Atoms,
                                                                    basisSet.Code,
                                                                        molecule.Charge.GetValueOrDefault()));
            }
        }

        private void CreateGeoDiskChargeFile(string directory, Molecule molecule, BasisSet basisSet)
        {
            string fileName = $"geodisk_{basisSet.Code}_{molecule.NameInfo}.inp";
            var inputFiles = QbcFile.FindFiles(directory, fileName);
            if (!inputFiles.Any())
            {
                QbcFile.WriteText(Path.Combine(directory, fileName),
                                GmsInputGenerator.GenGeoDiskChargeInput(molecule.Atoms,
                                                                    basisSet.Code,
                                                                        molecule.Charge.GetValueOrDefault()));
            }
        }

        private void CreateFukuiFiles(string directory, Molecule molecule, BasisSet basisSet)
        {
            string fileNameNeutral = $"fukui_{basisSet.Code}_{molecule.NameInfo}.inp";
            string fileNameLewisBase = $"fukuilewisbase_{basisSet.Code}_{molecule.NameInfo}.inp";
            string fileNameLewisAcid = $"fukuilewisacid_{basisSet.Code}_{molecule.NameInfo}.inp";
            var inputFiles = QbcFile.FindFiles(directory, fileNameNeutral);
            if (!inputFiles.Any())
            {
                var fukuiFiles = GmsInputGenerator.GenFukuiInput(molecule.Atoms, basisSet.Code, molecule.Charge.GetValueOrDefault());

                QbcFile.WriteText(Path.Combine(directory, fileNameNeutral),fukuiFiles.NeutralInput);
                QbcFile.WriteText(Path.Combine(directory, fileNameLewisBase), fukuiFiles.BaseInput);
                QbcFile.WriteText(Path.Combine(directory, fileNameLewisAcid), fukuiFiles.AcidInput);
            }
        }
    }
}
