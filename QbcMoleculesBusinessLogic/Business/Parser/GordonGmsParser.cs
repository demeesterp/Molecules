using QbcMoleculesBusinessLogic.Business.Conversion;
using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Business.Parser
{
    public class GordonGmsParser : IGordonGmsParser
    {
        public void ParseCharge(List<string> gmsFile, Molecule molecule)
        {
            string StartTag = "          ELECTROSTATIC POTENTIAL";
            string StartChargedTag = " NET CHARGES:";
            string EndChargeTag = " -------------------------------------";
            string StartElpotTag = " NUMBER OF POINTS SELECTED FOR FITTING";
            string GeoDiscTag = "PTSEL=GEODESIC";
            string line = string.Empty;
            bool overallstart = false;
            bool startCharge = false;
            bool startElpot = false;
            bool isGeoDisc = false;
            int currentAtomPos = 1;
            for (int c = 0; c < gmsFile.Count; ++c)
            {
                line = gmsFile[c];

                if (line.Contains(StartTag))
                {
                    overallstart = true;
                }

                if (line.Contains(GeoDiscTag))
                {
                    isGeoDisc = true;
                }

                if (overallstart && line.Contains(StartChargedTag))
                {
                    startCharge = true;
                    c += 3;
                    continue;
                }



                if (overallstart && line.StartsWith(StartElpotTag))
                {
                    startElpot = true;
                    continue;
                }

                if (startElpot)
                {
                    if (String.IsNullOrWhiteSpace(line))
                    {
                        startElpot = false;
                        continue;
                    }

                    var data = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length > 6)
                    {
                        MoleculeElpot item = new MoleculeElpot()
                        {
                            PosX = Convert.ToDecimal(data[1]),
                            PosY = Convert.ToDecimal(data[2]),
                            PosZ = Convert.ToDecimal(data[3]),
                            Electronic = Convert.ToDecimal(data[4]),
                            Nuclear = Convert.ToDecimal(data[5]),
                            Total = Convert.ToDecimal(data[6]),
                            Type = isGeoDisc ? (int)ElPotType.GeoDisc : (int)ElPotType.CHelgG
                        };
                        molecule.ElPot.Add(item);
                    }
                }

                if (startCharge)
                {
                    if (line.Contains(EndChargeTag))
                    {
                        return;
                    }

                    var data = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length > 2)
                    {
                        string symbol = data[0];
                        decimal charge = QbcStringConvert.ToDecimal(data[1].Trim());
                        var atom = molecule.Atoms.Find(i => i.Position == currentAtomPos && i.Symbol == symbol);
                        if (atom != null)
                        {
                            if (isGeoDisc)
                            {
                                atom.GeoDiscCharge = charge;
                            }
                            else
                            {
                                atom.CHelpGCharge = charge;
                            }

                        }
                        ++currentAtomPos;
                    }
                }
            }
        }

        public void ParseGeoOpt(List<string> gmsFile, Molecule molecule)
        {
            string OptimizationResultTag = "***** EQUILIBRIUM GEOMETRY LOCATED *****";
            string line = string.Empty;
            bool start = false;
            int position = 1;
            for (int c = 0; c < gmsFile.Count; ++c)
            {
                line = gmsFile[c];
                if (line.Contains(OptimizationResultTag))
                {
                    start = true;
                    c += 3;
                    continue;
                }

                if (start)
                {
                    if (String.IsNullOrWhiteSpace(line))
                    {
                        break;
                    }

                    var newatom = GordonGmsParseUtilities.ParseOptAtomPosition(line);
                    newatom.Position = position;
                    newatom.Number = position;

                    var existingAtom = molecule.Atoms.Find((i) => i.Symbol == newatom.Symbol
                                                                && i.Position == position
                                                                && i.Number == position);
                    if (existingAtom != null)
                    {
                        existingAtom.PosX = newatom.PosX;
                        existingAtom.PosY = newatom.PosY;
                        existingAtom.PosZ = newatom.PosZ;
                        existingAtom.AtomicWeight = newatom.AtomicWeight;
                    }
                    else
                    {
                        molecule.Atoms.Add(newatom);
                    }

                    ++position;
                }
            }
        }

        public void ParseGeoptDftEnergy(List<string> gmsFile, Molecule molecule)
        {
            string OptimizationResultTag = "***** EQUILIBRIUM GEOMETRY LOCATED *****";
            string EnergyStartTag = "          ENERGY COMPONENTS";
            string EnergyTag = "                       TOTAL ENERGY";

            bool start = false;
            bool overallstart = false;
            string line = "";
            for (int c = 0; c < gmsFile.Count; ++c)
            {
                line = gmsFile[c];
                if (line.Contains(OptimizationResultTag))
                {
                    overallstart = true;
                }

                if (overallstart && line.Contains(EnergyStartTag))
                {
                    start = true;
                }

                if (start && line.Contains(EnergyTag))
                {
                    var data = line.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length > 1)
                    {
                        molecule.DftEnergy = QbcStringConvert.ToDecimal(data[1].Trim());

                        break;
                    }
                }
            }
        }

        public void ParseGmsInput(List<string> gmsInputFile, Molecule molecule)
        {
            string GmsInputDataTag = "$Data";
            int start = gmsInputFile.FindIndex(i => i == GmsInputDataTag);
            int position = 1;
            for (int pos = start + 1; pos < gmsInputFile.Count; ++pos)
            {
                var result = gmsInputFile[pos].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (result.Length == 5)
                {
                    molecule.Atoms.Add(new MoleculeAtom()
                    {
                        Symbol = result[0],
                        AtomicWeight = (int)QbcStringConvert.ToDecimal(result[1]),
                        PosX = QbcStringConvert.ToDecimal(result[2]),
                        PosY = QbcStringConvert.ToDecimal(result[3]),
                        PosZ = QbcStringConvert.ToDecimal(result[4]),
                        Position = position++
                    });
                }
            }
        }

        public bool CheckValid(List<string> content)
        {
            return content.Any(i => i.Contains("EXECUTION OF GAMESS TERMINATED NORMALLY"));
        }


        public void ParseFukui(List<string> fukuiNeutral,
                                List<string> fukuiacid,
                                        List<string> fukuibase, Molecule molecule)
        {
            // neutral parsing
            new NeutralPopulationAnalysisParser().Parse(fukuiNeutral, molecule);

            decimal neutralEnergy = new FukuiEnergyNeutralParser().Parse(fukuiNeutral);

            // acid parsing
            new LewisAcidPopulationAnalysisParser().Parse(fukuiacid, molecule);

            decimal acidEnergy = new FukuiEnergyLewisAcidParser().Parse(fukuiacid);

            // base parsing
            new LewisBasePopulationAnalysisParser().Parse(fukuibase, molecule);

            decimal baseEnergy = new FukuiEnergyLewisBaseParser().Parse(fukuibase);

            decimal I = baseEnergy - neutralEnergy;
            decimal A = neutralEnergy - acidEnergy;

            molecule.HFEnergy = neutralEnergy;
            molecule.ElectronAffinity = (I + A) / 2;
            molecule.Hardness = (I - A) / 2;
        }



        #region private helpers



        #endregion
    }
}
