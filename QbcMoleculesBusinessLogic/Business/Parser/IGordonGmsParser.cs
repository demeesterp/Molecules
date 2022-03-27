using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Business.Parser
{
    public interface IGordonGmsParser
    {
        void ParseGeoOpt(List<string> gmsFile, Molecule molecule);

        void ParseGeoptDftEnergy(List<string> gmsFile, Molecule molecule);

        void ParseCharge(List<string> gmsFile, Molecule molecule);

        void ParseGmsInput(List<string> mgsInputFile, Molecule molecule);

        bool CheckValid(List<string> content);


        void ParseFukui(List<string> fukuiNeutral,
                            List<string> fukuiacid,
                                List<string> fukuibase, Molecule molecule);

    }
}
