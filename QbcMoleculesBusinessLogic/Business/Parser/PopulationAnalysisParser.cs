using QbcMoleculesBusinessLogic.Business.Conversion;
using QbcMoleculesBusinessLogic.Data.Molecules;

namespace QbcMoleculesBusinessLogic.Business.Parser
{

    public enum PopulationAnalysisType
    {
        neutral,
        lewisacid,
        lewisbase
    }


    public abstract class PopulationAnalysisParser
    {
        #region private properties

        private bool Start
        {
            get;
            set;
        }

        #endregion


        #region abstract tag methods

        protected abstract string GetGeometryResultTag();

        protected abstract string GetStartTag();

        protected abstract string GetStartTagAOPopulations();

        protected abstract string GetStartTagOverlapPopulations();

        protected abstract string GetStartTagPopulations();

        protected abstract string GetStartTagBondOrder();

        protected abstract PopulationAnalysisType GetPopulationStatus();

        #endregion

        public bool Parse(List<string> input, Molecule molecule)
        {
            bool retval = false;
            string line = string.Empty;
            bool overallstart = false;
            for (int c = 0; c < input.Count; ++c)
            {
                line = input[c];
                if (line.Contains(GetGeometryResultTag()))
                {
                    overallstart = true;
                }

                if (overallstart && line.Contains(GetStartTag()))
                {
                    Start = true;
                }

                if (Start)
                {
                    ParseAOPopulations(input.GetRange(c, input.Count - c), molecule);
                    ParseOverlapPopulations(input.GetRange(c, input.Count - c), molecule);
                    ParseBondOrder(input.GetRange(c, input.Count - c), molecule);
                    ParsePopulations(input.GetRange(c, input.Count - c), molecule);
                    retval = true;
                    break;
                }
            }

            return retval;

        }


        #region private helpers

        private bool CompareMoleculeAtomPos(MoleculeAtom lhs, MoleculeAtom rhs)
        {

            var result = (lhs.PosX - rhs.PosX) * (lhs.PosX - rhs.PosX) +
                         (lhs.PosY - rhs.PosY) * (lhs.PosY - rhs.PosY) +
                         (lhs.PosZ - rhs.PosZ) * (lhs.PosZ - rhs.PosZ);

            return result < 0.01M;

        }

        private void ParseAOPopulations(List<string> input, Molecule molecule)
        {
            if (this.Start)
            {
                bool startAOPop = false;
                string line = string.Empty;
                for (int c = 0; c < input.Count; ++c)
                {
                    line = input[c];
                    if (line.Contains(GetStartTagAOPopulations()))
                    {
                        startAOPop = true;
                        continue;
                    }

                    if (startAOPop)
                    {
                        var items = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                        if (items.Length == 6)
                        {
                            int atomPosition = int.Parse(items[2]);
                            string atomSymbol = items[1].Trim();
                            int orbitalpos = int.Parse(items[0]);
                            string orbitalSymbol = items[3];

                            MoleculeAtom atom = molecule.Atoms.Find(i => i.Position == atomPosition && i.Symbol == atomSymbol);
                            if (atom != null)
                            {
                                decimal mulliken = QbcStringConvert.ToDecimal(items[4].Trim());
                                decimal lowdin = QbcStringConvert.ToDecimal(items[5].Trim());
                                MoleculeAtomOrbital orbital = atom.Orbitals.Find(i => i.Position == orbitalpos && i.Symbol == orbitalSymbol);
                                if (orbital != null)
                                {
                                    switch (GetPopulationStatus())
                                    {
                                        case PopulationAnalysisType.neutral:
                                            {
                                                orbital.MullikenPopulation = mulliken;
                                                orbital.LowdinPopulation = lowdin;
                                                break;
                                            }
                                        case PopulationAnalysisType.lewisacid:
                                            {
                                                orbital.MullikenPopulationAcid = mulliken - orbital.MullikenPopulation;
                                                orbital.LowdinPopulationAcid = lowdin - orbital.LowdinPopulation;
                                                break;
                                            }
                                        case PopulationAnalysisType.lewisbase:
                                            {
                                                orbital.MullikenPopulationBase = orbital.MullikenPopulation - mulliken;
                                                orbital.LowdinPopulationBase = orbital.LowdinPopulation - lowdin;
                                                break;
                                            }
                                    }
                                }
                                else
                                {
                                    switch (GetPopulationStatus())
                                    {
                                        case PopulationAnalysisType.neutral:
                                            {
                                                atom.Orbitals.Add(new MoleculeAtomOrbital()
                                                {
                                                    Symbol = orbitalSymbol,
                                                    Position = orbitalpos,
                                                    LowdinPopulation = lowdin,
                                                    MullikenPopulation = mulliken
                                                });
                                                break;
                                            }
                                        case PopulationAnalysisType.lewisacid:
                                            {
                                                atom.Orbitals.Add(new MoleculeAtomOrbital()
                                                {
                                                    Symbol = orbitalSymbol,
                                                    Position = orbitalpos,
                                                    LowdinPopulationAcid = lowdin,
                                                    MullikenPopulationAcid = mulliken
                                                });
                                                break;
                                            }
                                        case PopulationAnalysisType.lewisbase:
                                            {
                                                atom.Orbitals.Add(new MoleculeAtomOrbital()
                                                {
                                                    Symbol = orbitalSymbol,
                                                    Position = orbitalpos,
                                                    LowdinPopulationBase = lowdin,
                                                    MullikenPopulationBase = mulliken
                                                });
                                                break;
                                            }
                                    }
                                }
                            }
                        }


                        if (String.IsNullOrWhiteSpace(line))
                        {
                            break;
                        }
                    }

                }
            }

        }


        private void ParseOverlapPopulations(List<string> input, Molecule molecule)
        {
            if (this.Start)
            {
                bool startAOPop = false;
                string line = string.Empty;
                MoleculeBond[,] bondmatrix = new MoleculeBond[molecule.Atoms.Count, molecule.Atoms.Count];
                int blockCount = 0;
                bool startBlockcounting = false;
                for (int c = 0; c < input.Count; ++c)
                {
                    line = input[c];
                    if (line.Contains(GetStartTagOverlapPopulations()))
                    {
                        startAOPop = true;
                        c += 1;
                        continue;
                    }
                    if (startAOPop)
                    {
                        if (line.Contains(GetStartTagPopulations()))
                        {
                            break;
                        }
                        var data = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        if (data.Length > 1)
                        {
                            if (data[1].Contains("."))
                            {
                                startBlockcounting = true;
                                for (int pos = 1; pos < data.Length; ++pos)
                                {

                                    switch (GetPopulationStatus())
                                    {
                                        case PopulationAnalysisType.neutral:
                                            {
                                                bondmatrix[int.Parse(data[0]) - 1, blockCount * 5 + pos - 1] = new MoleculeBond()
                                                {
                                                    Atom1Position = int.Parse(data[0]),
                                                    Atom2Position = blockCount * 5 + pos,
                                                    OverlapPopulation = QbcStringConvert.ToDecimal(data[pos].Trim())
                                                };
                                                break;
                                            }
                                        case PopulationAnalysisType.lewisacid:
                                            {
                                                bondmatrix[int.Parse(data[0]) - 1, blockCount * 5 + pos - 1] = new MoleculeBond()
                                                {
                                                    Atom1Position = int.Parse(data[0]),
                                                    Atom2Position = blockCount * 5 + pos,
                                                    OverlapPopulationAcid = QbcStringConvert.ToDecimal(data[pos].Trim())
                                                };


                                                break;
                                            }
                                        case PopulationAnalysisType.lewisbase:
                                            {
                                                bondmatrix[int.Parse(data[0]) - 1, blockCount * 5 + pos - 1] = new MoleculeBond()
                                                {
                                                    Atom1Position = int.Parse(data[0]),
                                                    Atom2Position = blockCount * 5 + pos,
                                                    OverlapPopulationBase = QbcStringConvert.ToDecimal(data[pos].Trim())
                                                };
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                        if (startBlockcounting && String.IsNullOrWhiteSpace(line))
                        {
                            ++blockCount;
                            startBlockcounting = false;
                        }
                    }
                }

                foreach (var bond in bondmatrix)
                {
                    if (bond != null && bond.Atom1Position != bond.Atom2Position)
                    {

                        var found = molecule.Bonds.Find(i => (i.Atom1Position == bond.Atom1Position && i.Atom2Position == bond.Atom2Position)
                                                             ||
                                                             (i.Atom1Position == bond.Atom2Position && i.Atom2Position == bond.Atom1Position));

                        if (found != null)
                        {
                            if (GetPopulationStatus() == PopulationAnalysisType.neutral)
                            {
                                found.OverlapPopulation = bond.OverlapPopulation;
                            }
                            if (GetPopulationStatus() == PopulationAnalysisType.lewisacid)
                            {
                                found.OverlapPopulationAcid = bond.OverlapPopulationAcid - found.OverlapPopulation;
                            }
                            if (GetPopulationStatus() == PopulationAnalysisType.lewisbase)
                            {
                                found.OverlapPopulationBase = found.OverlapPopulation - bond.OverlapPopulationBase;
                            }
                        }
                        else
                        {
                            molecule.Bonds.Add(bond);
                        }
                    }
                }

                foreach (var bond in molecule.Bonds)
                {
                    var atom = molecule.Atoms.Find(i => i.Position == bond.Atom1Position);
                    if (atom != null)
                    {
                        var atombond = atom.Bonds.Find(i => i.Atom1Position == bond.Atom1Position && i.Atom2Position == bond.Atom2Position);
                        if (atombond != null)
                        {
                            atom.Bonds.Remove(atombond);
                        }
                        atom.Bonds.Add(bond);
                    }
                }
            }
        }


        private void ParseBondOrder(List<string> input, Molecule molecule)
        {
            if (this.Start)
            {
                bool startPop = false;
                string line = string.Empty;
                for (int c = 0; c < input.Count; ++c)
                {
                    line = input[c];
                    if (line.Contains(GetStartTagBondOrder()))
                    {
                        startPop = true;
                        c += 4;
                        continue;
                    }


                    if (startPop)
                    {

                        if (String.IsNullOrWhiteSpace(line))
                        {
                            break;
                        }

                        var result = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                        for (int outerc = 0; outerc < result.Length / 4; ++outerc)
                        {
                            int atompos1 = int.Parse(result[outerc * 4]);
                            int atompos2 = int.Parse(result[1 + outerc * 4]);
                            decimal dist = QbcStringConvert.ToDecimal(result[2 + outerc * 4].Trim());
                            decimal bondorder = QbcStringConvert.ToDecimal(result[3 + outerc * 4].Trim());

                            switch (GetPopulationStatus())
                            {
                                case PopulationAnalysisType.neutral:
                                    {
                                        var b = molecule.Bonds.Find(i => (i.Atom1Position == atompos1 && i.Atom2Position == atompos2) || (i.Atom1Position == atompos2 && i.Atom2Position == atompos1));
                                        if (b != null)
                                        {
                                            b.BondOrder = bondorder;
                                            b.Distance = dist;
                                        }
                                        break;
                                    }
                                case PopulationAnalysisType.lewisacid:
                                    {
                                        var b = molecule.Bonds.Find(i => (i.Atom1Position == atompos1 && i.Atom2Position == atompos2) || (i.Atom1Position == atompos2 && i.Atom2Position == atompos1));
                                        if (b != null)
                                        {
                                            b.BondOrderAcid = bondorder - b.BondOrder;
                                            b.Distance = dist;
                                        }
                                        break;
                                    }
                                case PopulationAnalysisType.lewisbase:
                                    {
                                        var b = molecule.Bonds.Find(i => (i.Atom1Position == atompos1 && i.Atom2Position == atompos2) || (i.Atom1Position == atompos2 && i.Atom2Position == atompos1));
                                        if (b != null)
                                        {
                                            b.BondOrderBase = b.BondOrder - bondorder;
                                            b.Distance = dist;
                                        }
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
        }


        private void ParsePopulations(List<string> input, Molecule molecule)
        {
            if (this.Start)
            {
                bool startPop = false;
                string line = string.Empty;
                for (int c = 0; c < input.Count; ++c)
                {
                    line = input[c];
                    if (line.Contains(GetStartTagPopulations()))
                    {
                        startPop = true;
                        continue;
                    }

                    if (startPop)
                    {
                        var result = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        if (result.Length > 5)
                        {
                            int position = int.Parse(result[0].Trim());
                            string symbol = result[1].Trim();
                            decimal mullpop = QbcStringConvert.ToDecimal(result[2].Trim());
                            decimal lowdinpop = QbcStringConvert.ToDecimal(result[4].Trim());


                            switch (GetPopulationStatus())
                            {
                                case PopulationAnalysisType.neutral:
                                    {
                                        var atom = molecule.Atoms.Find(i => i.Position == position && i.Symbol == symbol);
                                        if (atom != null)
                                        {
                                            atom.MullikenPopulation = mullpop;
                                            atom.LowdinPopulation = lowdinpop;
                                        }
                                        break;
                                    }
                                case PopulationAnalysisType.lewisacid:
                                    {
                                        var atom = molecule.Atoms.Find(i => i.Position == position && i.Symbol == symbol);
                                        if (atom != null)
                                        {
                                            atom.MullikenPopulationAcid = mullpop - atom.MullikenPopulation;
                                            atom.LowdinPopulationAcid = lowdinpop - atom.LowdinPopulation;
                                        }
                                        break;
                                    }
                                case PopulationAnalysisType.lewisbase:
                                    {
                                        var atom = molecule.Atoms.Find(i => i.Position == position && i.Symbol == symbol);
                                        if (atom != null)
                                        {
                                            atom.MullikenPopulationBase = atom.MullikenPopulation - mullpop;
                                            atom.LowdinPopulationBase = atom.LowdinPopulation - lowdinpop;
                                        }
                                        break;
                                    }
                            }
                        }
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            break;
                        }
                    }

                }

            }

        }


        #endregion
    }
}
