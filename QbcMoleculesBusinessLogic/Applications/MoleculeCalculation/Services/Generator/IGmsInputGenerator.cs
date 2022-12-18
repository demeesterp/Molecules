using QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.ProcessCommands;
using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.Generator
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
