using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Data.Molecules
{
    public enum MoleculeErrorStatus
    {
        NoInfo = 0,
        Ok = 1,
        Error = 2
    }

    public class Molecule
    {

        public Molecule()
        {
            Bonds = new List<MoleculeBond>();
            Atoms = new List<MoleculeAtom>();
            ElPot = new List<MoleculeElpot>();
        }

        public int Id { get; set; }

        public string NameInfo { get; set;} = String.Empty;

        public string Description { get; set; } = String.Empty;

        public string Comment { get; set; } = String.Empty;

        public MoleculeErrorStatus Status { get; set; }

        public List<MoleculeBond> Bonds { get; set; }

        public List<MoleculeAtom> Atoms { get; set; }

        public List<MoleculeElpot> ElPot { get; set; }

        public int? Charge { get; set; }

        public decimal? DftEnergy { get; set; }

        public decimal? HFEnergy { get; set; }

        public decimal? ElectronAffinity {  get; set; }

        public decimal? Hardness { get; set; }

    }
}
