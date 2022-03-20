using QbcMoleculesBusinessLogic.Data.Molecules;
using QbcMoleculesBusinessLogic.Data.ProcessCommands;

namespace QbcMoleculesBusinessLogic.Business.Generator
{
    public interface IGmsInputGenerator
    {

        string GenCHelpGChargeInput(List<MoleculeAtom> positions, string basissetCode, int charge);

        string GenGeoDiskChargeInput(List<MoleculeAtom> positions, string basissetCode, int charge);

        string GenGeoOptInput(List<MoleculeAtom> positions, string basissetCode, int charge);

        FukuiGmsInput GenFukuiInput(List<MoleculeAtom> positions, string basissetCode, int charge);

        string GenXyzInput(List<MoleculeAtom> positions);
    }
}
