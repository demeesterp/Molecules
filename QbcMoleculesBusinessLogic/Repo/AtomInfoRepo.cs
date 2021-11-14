using QbcMoleculesBusinessLogic.Data.Molecules;
using QbcMoleculesBusinessLogic.Repo.Resources;

namespace QbcMoleculesBusinessLogic.Repo
{
    public class AtomInfoRepo : IAtomInfoRepo
    {
        public const string ResourceName = "QbcMoleculesBusinessLogic.Data.DataFiles.atominfo.txt";

        #region dependencies

        private IQbcResource Resource { get; }

        #endregion

        public AtomInfoRepo(IQbcResource resource)
        {
            Resource = resource;
        }

        public List<Atom> GetAtomInfo()
        {
            List<Atom> retval = new();
            string[]? filedata = Resource.ReadResource(ResourceName)?.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            Atom current;
            if ( filedata != null)
            {
                foreach (var line in filedata.Skip(1))
                {
                    string[] records = line.Split('\t', StringSplitOptions.RemoveEmptyEntries);
                    if (records.Length == 8)
                    {
                        current = new Atom
                        {
                            ID = int.Parse(records[0]),
                            Symbol = records[1],
                            Name = records[2],
                            AtomNumber = records[3],
                            AtomMass = records[4],
                            AtomRadius = records[5],
                            ElectronAffinity = records[6]
                        };
                        current.ElectronAffinity = records[7];
                        retval.Add(current);
                    }
                }
            }
            return retval;
        }
    }
}
