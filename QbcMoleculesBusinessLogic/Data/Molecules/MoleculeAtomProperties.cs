namespace QbcMoleculesBusinessLogic.Data.Molecules
{
    public class MoleculeAtomProperties
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty ;
        public int AtomNumber { get; set; }
        public decimal? AtomMass { get; set; }
        public decimal? AtomRadius { get; set; }
        public decimal? ElectroNegativity { get; set; }
        public decimal? ElectronAffinity { get; set; }
    }
}
