using QbcMoleculesBusinessLogic.Business.Generator;
using QbcMoleculesBusinessLogic.Business.Logging;
using QbcMoleculesBusinessLogic.Business.Parser;
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

        private IGordonGmsParser GordonGmsParser { get; }

        #endregion


        public MolCalcCoordCmd(IQbcFile qbcFile,
                                IMoleculeFileRepo moleculeFileRepo,
                                IUserInteractionService userInteractionService,
                                IGmsInputGenerator gmsInputGenerator,
                                IGordonGmsParser gordonGmsParser,
                                IQbcLogger logger)
        {
            QbcFile = qbcFile;
            Logger = logger;
            MoleculeFileRepo = moleculeFileRepo;
            UserInteractionService = userInteractionService;
            GmsInputGenerator = gmsInputGenerator;
            GordonGmsParser =   gordonGmsParser;
        }



        public Task<CalcCoordResult> ProcessAsync(CalcCoordInfo info)
        {
            CalcCoordResult retval = new();
            
            BasisSet? basisset = this.UserInteractionService.SelectBasisSet();
            
            bool needGeoOpt = this.UserInteractionService.NeedGeoOpt();
            
            if (basisset != null)
            {
                foreach (string moleculeFile in QbcFile.FindFiles(info.BasePath, "*.json"))
                {
                    Molecule? mol = this.MoleculeFileRepo.ReadFromFile(moleculeFile);
                    if ( mol != null)
                    {
                        if (needGeoOpt)
                        {
                            CreateGeoOptFile(info.BasePath, mol, basisset);
                            ParseGeoOptFile(info.BasePath, mol, basisset);
                        }
                        
                        CreateCHelpGChargeFile(info.BasePath, mol, basisset);
                        ParseCHelpGChargeFile(info.BasePath, mol, basisset);
                        
                        CreateGeoDiskChargeFile(info.BasePath, mol, basisset);
                        ParseGeoDiskChargeFile(info.BasePath, mol, basisset);
                        
                        CreateFukuiFiles(info.BasePath, mol, basisset);
                        ParseFukuiFiles(info.BasePath, mol, basisset);

                        this.MoleculeFileRepo.WriteToFile(mol, info.BasePath);
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

        private void ParseGeoOptFile(string directory, Molecule molecule, BasisSet basisSet)
        {
            string fileName = $"geoopt_{basisSet.Code}_{molecule.NameInfo}.log";
            var outputFiles = QbcFile.FindFiles(directory, fileName);
            if (outputFiles.Count == 1)
            {
                var lines = QbcFile.ReadLines(outputFiles.First());
                if ( GordonGmsParser.CheckValid(lines) )
                {
                    GordonGmsParser.ParseGeoOpt(lines, molecule);
                };
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

        private void ParseCHelpGChargeFile(string directory, Molecule molecule, BasisSet basisSet)
        {
            string fileName = $"chelpg_{basisSet.Code}_{molecule.NameInfo}.inp";
            var outputFiles = QbcFile.FindFiles(directory, fileName);
            if (outputFiles.Count == 1)
            {

                var lines = QbcFile.ReadLines(outputFiles.First());
                if (GordonGmsParser.CheckValid(lines))
                {
                    GordonGmsParser.ParseCharge(lines, molecule);
                };
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

        private void ParseGeoDiskChargeFile(string directory, Molecule molecule, BasisSet basisSet)
        {
            string fileName = $"geodisk_{basisSet.Code}_{molecule.NameInfo}.inp";
            var outputFiles = QbcFile.FindFiles(directory, fileName);
            if (outputFiles.Count == 1)
            {

                var lines = QbcFile.ReadLines(outputFiles.First());
                if (GordonGmsParser.CheckValid(lines))
                {
                    GordonGmsParser.ParseCharge(lines, molecule);
                };
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

        private void ParseFukuiFiles(string directory, Molecule molecule, BasisSet basisSet)
        {
            string fileNameNeutral = $"fukui_{basisSet.Code}_{molecule.NameInfo}.log";
            string fileNameLewisBase = $"fukuilewisbase_{basisSet.Code}_{molecule.NameInfo}.log";
            string fileNameLewisAcid = $"fukuilewisacid_{basisSet.Code}_{molecule.NameInfo}.log";
            
            var neutralOutputFiles = QbcFile.FindFiles(directory, fileNameNeutral);
            var acidOutputFiles = QbcFile.FindFiles(directory, fileNameLewisAcid);
            var baseOutputFiles = QbcFile.FindFiles(directory, fileNameLewisBase);

            if ( neutralOutputFiles.Count == 1 
                        && acidOutputFiles.Count == 1 
                        && baseOutputFiles.Count == 1)
            {
                var neutral = QbcFile.ReadLines(neutralOutputFiles.First());
                var lewisacid = QbcFile.ReadLines(acidOutputFiles.First());
                var lewisbase = QbcFile.ReadLines(baseOutputFiles.First());

                if ( GordonGmsParser.CheckValid(neutral) 
                            && GordonGmsParser.CheckValid(lewisacid)
                             && GordonGmsParser.CheckValid(lewisbase))
                {
                    GordonGmsParser.ParseFukui(neutral, lewisacid, lewisbase, molecule);
                }
            }
        }
    }
}
