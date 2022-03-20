using QbcMoleculesBusinessLogic.Business.Generator;
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

        private IQbcLogger Logger { get; }

        private IQbcFile QbcFile { get; }

        private IMoleculeFileRepo MoleculeFileRepo { get; }

        private IBasissetInfoRepo BasissetInfoRepo { get; }

        private IGmsInputGenerator GmsInputGenerator { get; }

        #endregion


        public MolCalcCoordCmd(IQbcFile qbcFile,
                                IMoleculeFileRepo moleculeFileRepo,
                                IBasissetInfoRepo basissetInfoRepo,
                                IGmsInputGenerator gmsInputGenerator,
                                IQbcLogger logger)
        {
            QbcFile = qbcFile;
            Logger = logger;
            MoleculeFileRepo = moleculeFileRepo;
            BasissetInfoRepo = basissetInfoRepo;
            GmsInputGenerator = gmsInputGenerator;
        }



        public Task<CalcCoordResult> ProcessAsync(CalcCoordInfo info)
        {
            CalcCoordResult retval = new();
            foreach (string moleculeDir in QbcFile.FindDirectories(info.BasePath, "*"))
            {
                Molecule? result = MoleculeFileRepo.ReadFromFile(Path.Combine(moleculeDir, $"{Path.GetDirectoryName(moleculeDir)}.json"));
                CreateGeoOptFile(moleculeDir, result);



            };
            return Task.FromResult(retval);
        }


        private void CreateGeoOptFile(string directory, Molecule molecule)
        {
            foreach(var basisset in BasissetInfoRepo.GetBasisSetInfo())
            {
                string fileName = $"geoopt_{basisset.Code}_{molecule.NameInfo}.inp";
                var inputFiles = QbcFile.FindFiles(directory, fileName);
                if ( !inputFiles.Any())
                {
                    QbcFile.WriteText(Path.Combine(directory, fileName),
                                    GmsInputGenerator.GenGeoOptInput(molecule.Atoms, 
                                                                        basisset.Code, 
                                                                            molecule.Charge.GetValueOrDefault()));
                }
            }
        }
    }
}
