using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Business.ProcessingCommand
{
    public class MolCalcCmd : IMolCalcCmd
    {

        #region dependencies


        private IMolCalcInitCmd         MolCalcInitCmd { get; } 


        private IMolCalcCoordCmd        MolCalcCoordCmd { get; }


        #endregion


        public MolCalcCmd(IMolCalcInitCmd molCalcInitCmd,
                            IMolCalcCoordCmd molCalcCoordCmd)
        {
            MolCalcInitCmd = molCalcInitCmd;
            MolCalcCoordCmd = molCalcCoordCmd;
        }


        public async Task<List<Molecule>> ProcessAsync(string cmdInfo)
        {
            List<Molecule> retval = new ();

            _ =  await this.MolCalcInitCmd.ProcessAsync(cmdInfo);

            await this.MolCalcCoordCmd.ProcessAsync(cmdInfo);

            
            return retval;
        }


    }
}
