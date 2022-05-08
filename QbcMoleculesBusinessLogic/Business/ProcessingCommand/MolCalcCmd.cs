using QbcMoleculesBusinessLogic.Business.Logging;
using QbcMoleculesBusinessLogic.Data.CmdArgs;
using QbcMoleculesBusinessLogic.Data.CmdArgs.Processing;

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


        public async Task<MolCalcCmdResult> ProcessAsync(MolCalcCmdInfo cmdInfo)
        {
            MolCalcCmdResult retval = new ();

            _ =  await this.MolCalcInitCmd.ProcessAsync(new CalcInitInfo()
            {
                BaseDir = cmdInfo.BasePath
            });

            await this.MolCalcCoordCmd.ProcessAsync(new CalcCoordInfo()
            {
                BasePath = cmdInfo.BasePath
            });

            
            return retval;
        }


    }
}
