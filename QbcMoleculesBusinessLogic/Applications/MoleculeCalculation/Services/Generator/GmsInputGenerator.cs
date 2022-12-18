using QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.ProcessCommands;
using QbcMoleculesBusinessLogic.Data.DataFiles;
using QbcMoleculesBusinessLogic.Data.Molecules;
using QbcMoleculesBusinessLogic.Repo;
using System.Text;

namespace QbcMoleculesBusinessLogic.Applications.MoleculeCalculation.Services.Generator
{
    public class GmsInputGenerator : IGmsInputGenerator
    {
        #region dependencies

        public IBasissetInfoRepo BasisSetInfoRepo { get; }

        public IAtomInfoRepo AtomInfoRepo { get; }

        #endregion

        public GmsInputGenerator(IBasissetInfoRepo basissetInfoRepo, IAtomInfoRepo atomInfoRepo)
        {
            BasisSetInfoRepo = basissetInfoRepo;
            AtomInfoRepo = atomInfoRepo;
        }


        public string GenCHelpGChargeInput(List<MoleculeAtom> positions, string basissetCode, int charge)
        {
            StringBuilder retval = new StringBuilder();
            var basisSet = BasisSetInfoRepo.GetBasisSetInfo().Find(i => i.Code == basissetCode);
            retval.AppendLine($" {basisSet?.GmsInput}");
            retval.AppendLine($" $CONTRL SCFTYP=RHF DFTTYP=B3LYP MAXIT=60 MULT=1 ICHARG={charge} $END");
            retval.AppendLine(" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            retval.AppendLine(" $SCF DIRSCF=.TRUE. $END");
            retval.AppendLine(" $ELPOT  IEPOT=1 WHERE=PDC $END");
            retval.AppendLine(" $PDC PTSEL=CHELPG CONSTR=CHARGE $END");
            retval.AppendLine(" $DATA");
            retval.AppendLine();
            retval.AppendLine("C1");
            foreach (var atom in positions)
            {
                Atom? current = AtomInfoRepo.GetAtomInfo().Find((i) => i.Symbol == atom.Symbol);
                retval.AppendLine($"{atom.Symbol} {current?.AtomMass:0.0} {atom.PosX} {atom.PosY} {atom.PosZ}".Replace(",", "."));
            }
            retval.AppendLine(" $END");
            return retval.ToString();
        }

        public string GenGeoDiskChargeInput(List<MoleculeAtom> positions, string basissetCode, int charge)
        {
            StringBuilder retval = new StringBuilder();
            var basisSet = BasisSetInfoRepo.GetBasisSetInfo().Find(i => i.Code == basissetCode);
            retval.AppendLine($" {basisSet?.GmsInput}");
            retval.AppendLine($" $CONTRL SCFTYP=RHF DFTTYP=B3LYP MAXIT=60 MULT=1 ICHARG={charge} $END");
            retval.AppendLine(" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            retval.AppendLine(" $SCF DIRSCF=.TRUE. $END");
            retval.AppendLine(" $ELPOT  IEPOT=1 WHERE=PDC $END");
            retval.AppendLine(" $PDC PTSEL=GEODESIC CONSTR=CHARGE $END");
            retval.AppendLine(" $DATA");
            retval.AppendLine();
            retval.AppendLine("C1");
            foreach (var atom in positions)
            {
                Atom? current = AtomInfoRepo.GetAtomInfo().Find((i) => i.Symbol == atom.Symbol);
                retval.AppendLine($"{atom.Symbol} {current?.AtomMass:0.0} {atom.PosX} {atom.PosY} {atom.PosZ}".Replace(",", "."));
            }
            retval.AppendLine(" $END");
            return retval.ToString();
        }


        public string GenGeoOptInput(List<MoleculeAtom> moleculeAtoms, string basissetCode, int charge)
        {
            StringBuilder retval = new StringBuilder();
            var basisSet = BasisSetInfoRepo.GetBasisSetInfo().Find(i => i.Code == basissetCode);
            retval.AppendLine($" {basisSet?.GmsInput}");
            retval.AppendLine($" $CONTRL SCFTYP=RHF RUNTYP=OPTIMIZE DFTTYP=B3LYP MAXIT=60 MULT=1 ICHARG={charge} $END ");
            retval.AppendLine(" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            retval.AppendLine(" $STATPT NSTEP=999 $END");
            retval.AppendLine($" $SCF DIRSCF=.TRUE. $END");
            retval.AppendLine(" $DATA");
            retval.AppendLine();
            retval.AppendLine("C1");
            foreach (var atom in moleculeAtoms)
            {
                Atom? current = AtomInfoRepo.GetAtomInfo().Find((i) => i.Symbol == atom.Symbol || i.Name == atom.Symbol);
                if (current != null)
                {
                    retval.AppendLine($"{atom.Symbol} {current.AtomMass:0.0} {atom.PosX} {atom.PosY} {atom.PosZ}".Replace(",", "."));
                }
                else
                {
                    throw new Exception($"unknown atom {atom.Symbol} calculation cannot proceed !");
                }
            }
            retval.AppendLine(" $END");
            return retval.ToString();
        }

        public FukuiGmsInput GenFukuiInput(List<MoleculeAtom> positions, string basissetCode, int charge)
        {
            FukuiGmsInput retval = new FukuiGmsInput();
            var basisSet = BasisSetInfoRepo.GetBasisSetInfo().Find(i => i.Code == basissetCode);

            var input = new StringBuilder();
            input.AppendLine($" {basisSet?.GmsInput}");
            input.AppendLine($" $CONTRL SCFTYP=RHF MAXIT=60 MULT=1 ICHARG={charge} $END");
            input.AppendLine($" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            input.AppendLine($" $SCF DIRSCF=.TRUE. $END");
            input.AppendLine(" $DATA");
            input.AppendLine();
            input.AppendLine("C1");
            foreach (var atom in positions)
            {
                Atom? current = AtomInfoRepo.GetAtomInfo().Find((i) => i.Symbol == atom.Symbol);
                input.AppendLine($"{atom.Symbol} {current?.AtomMass:0.0} {atom.PosX} {atom.PosY} {atom.PosZ}".Replace(",", "."));
            }
            input.AppendLine(" $END");

            retval.NeutralInput = input.ToString();

            // Lewis Base
            input.Clear();
            input.AppendLine($" {basisSet?.GmsInput}");
            input.AppendLine($" $CONTRL SCFTYP=UHF MAXIT=60 MULT=2 ICHARG={charge + 1} $END");
            input.AppendLine($" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            input.AppendLine($" $SCF DIRSCF=.TRUE. $END");
            input.AppendLine(" $STATPT OPTTOL=0.0001 NSTEP=999 $END");
            input.AppendLine(" $DATA");
            input.AppendLine();
            input.AppendLine("C1");
            foreach (var atom in positions)
            {
                Atom? current = AtomInfoRepo.GetAtomInfo().Find((i) => i.Symbol == atom.Symbol);
                input.AppendLine($"{atom.Symbol} {current?.AtomMass:0.0} {atom.PosX} {atom.PosY} {atom.PosZ}".Replace(",", "."));
            }
            input.AppendLine(" $END");

            retval.BaseInput = input.ToString();


            input.Clear();
            input.AppendLine($" {basisSet?.GmsInput}");
            input.AppendLine($" $CONTRL SCFTYP=UHF MAXIT=60 MULT=2 ICHARG={charge - 1} $END");
            input.AppendLine($" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            input.AppendLine($" $SCF DIRSCF=.TRUE. $END");
            input.AppendLine(" $STATPT OPTTOL=0.0001 NSTEP=999 $END");
            input.AppendLine(" $DATA");
            input.AppendLine();
            input.AppendLine("C1");
            foreach (var atom in positions)
            {
                Atom? current = AtomInfoRepo.GetAtomInfo().Find((i) => i.Symbol == atom.Symbol);
                input.AppendLine($"{atom.Symbol} {current?.AtomMass:0.0} {atom.PosX} {atom.PosY} {atom.PosZ}".Replace(",", "."));
            }

            input.AppendLine(" $END");

            retval.AcidInput = input.ToString();

            return retval;
        }

        public string GenXyzInput(List<MoleculeAtom> positions)
        {
            StringBuilder retval = new StringBuilder();
            if (positions.Count > 1)
            {
                retval.AppendLine($"{positions.Count}");
                retval.AppendLine();
                foreach (var ln in positions)
                {
                    retval.AppendLine($"{ln.Symbol} {ln.PosX} {ln.PosY} {ln.PosZ}");
                }
            }
            return retval.ToString();
        }


    }
}
