﻿using QbcMoleculesBusinessLogic.Business.Logging;
using QbcMoleculesBusinessLogic.Business.Parser;
using QbcMoleculesBusinessLogic.Data.CmdArgs;
using QbcMoleculesBusinessLogic.Data.CmdArgs.Processing;
using QbcMoleculesBusinessLogic.Data.Molecules;
using QbcMoleculesBusinessLogic.Repo;
using QbcMoleculesBusinessLogic.Repo.Files;

namespace QbcMoleculesBusinessLogic.Business.ProcessingCommand
{
    public class MolCalcInitCmd : IMolCalcInitCmd
    {

        #region dependencies

        private IQbcFile        QbcFile { get; }

        private IXyzParser      XyzParser { get; }

        private IQbcLogger      QbcLogger { get; }

        private IMoleculeFileRepo MoleculeFileRepo { get; }

        #endregion

        public MolCalcInitCmd(IQbcFile qbcFile, IXyzParser xyzParser, IMoleculeFileRepo moleculeFileRepo, IQbcLogger logger)
        {
            QbcFile = qbcFile ?? throw new ArgumentNullException(nameof(qbcFile));
            QbcLogger = logger ?? throw new ArgumentNullException(nameof(logger));
            XyzParser = xyzParser ?? throw new ArgumentNullException(nameof(xyzParser));
            MoleculeFileRepo = moleculeFileRepo ?? throw new ArgumentNullException(nameof(moleculeFileRepo));
        }

        public async Task<CalcInitResult> ProcessAsync(CalcInitInfo initInfo)
        {
            QbcLogger.LogInformation($"MolCalcInit {initInfo.BaseDir}");
            CalcInitResult calcInitResult = new();
            foreach (string xyzFile in QbcFile.FindFiles(initInfo.BaseDir, "*.xyz"))
            {
                string xyzdata = QbcFile.ReadText(xyzFile);
                Molecule? result = XyzParser.Parse(xyzdata);                
                if (result != null)
                {
                    string[] nameInfo = Path.GetFileNameWithoutExtension(xyzFile).Split("_", StringSplitOptions.RemoveEmptyEntries);
                    result.NameInfo = nameInfo.First();
                    if ( nameInfo.Length > 1 )
                    {
                        result.Charge =int.TryParse(nameInfo[1], out int charge) ? charge : 0;
                    }                    
                    if ( !MoleculeFileRepo.MoleculeExists(result, initInfo.BaseDir))
                    {
                        MoleculeFileRepo.WriteToFile(result, initInfo.BaseDir);
                    }
                    calcInitResult.Result.Add(result);
                }
            }
            return await Task.FromResult(calcInitResult);
        }
    }
}
