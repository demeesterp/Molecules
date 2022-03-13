using QbcMoleculesBusinessLogic.Business.Logging;
using QbcMoleculesBusinessLogic.Data.CmdArgs;

namespace QbcMoleculesBusinessLogic.Business.ProcessingCommand
{
    public class MolCalcCmd : IMolCalcCmd
    {

        #region dependencies

        private IQbcLogger              Logger { get; }


        private IMolCalcInitCmd         MolCalcInitCmd { get; } 


        private IMolCalcCoordCmd        MolCalcCoordCmd { get; }


        private IMolDataCompilationCmd  MolDataCompilationCmd { get; } 

        #endregion


        public MolCalcCmd(IMolCalcInitCmd molCalcInitCmd,
                            IMolCalcCoordCmd molCalcCoordCmd, 
                                IMolDataCompilationCmd molDataCompilationCmd,
                                  IQbcLogger logger  )
        {
            Logger = logger;
            MolCalcInitCmd = molCalcInitCmd;
            MolCalcCoordCmd = molCalcCoordCmd;
            MolDataCompilationCmd = molDataCompilationCmd;
        }


        public async Task<MolCalcCmdResult> ProcessAsync(MolCalcCmdInfo cmdInfo)
        {
            MolCalcCmdResult retval = new ();

            CalcInitResult result = await this.MolCalcInitCmd.ProcessAsync(new CalcInitInfo()
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
