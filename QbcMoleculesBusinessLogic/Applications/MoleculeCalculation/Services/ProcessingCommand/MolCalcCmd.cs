using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.ProcessingCommand
{
    public class MolCalcCmd : IMolCalcCmd
    {

        #region dependencies


        private IMolCalcInitCmd MolCalcInitCmd { get; }


        private IMolCalcCoordCmd MolCalcCoordCmd { get; }


        #endregion


        public MolCalcCmd(IMolCalcInitCmd molCalcInitCmd,
                            IMolCalcCoordCmd molCalcCoordCmd)
        {
            MolCalcInitCmd = molCalcInitCmd;
            MolCalcCoordCmd = molCalcCoordCmd;
        }


        public async Task<List<Molecule>> ProcessAsync(string basePath)
        {
            List<Molecule> retval = new();

            _ = await MolCalcInitCmd.ProcessAsync(basePath);

            await MolCalcCoordCmd.ProcessAsync(basePath);


            return retval;
        }


    }
}
