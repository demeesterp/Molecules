using QbcMoleculesBusinessLogic.Data.DataFiles;
using QbcMoleculesBusinessLogic.Repo.Resources;

namespace QbcMoleculesBusinessLogic.Repo
{
    public class BasissetInfoRepo : IBasissetInfoRepo
    {

        public const string ResourceName = "QbcMoleculesBusinessLogic.Data.DataFiles.basisset.txt";

        #region dependencies

        private IQbcResource Resource { get; }

        #endregion

        public BasissetInfoRepo(IQbcResource resource)
        {
            Resource = resource;
        }

        public List<BasisSet> GetBasisSetInfo()
        {
            List<BasisSet> retval = new();
            string[]? filedata = Resource.ReadResource(ResourceName)?.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            BasisSet current;
            if ( filedata != null)
            {
                foreach (var line in filedata.Skip(1))
                {
                    string[] records = line.Split('\t', StringSplitOptions.RemoveEmptyEntries);
                    if (records.Length == 4)
                    {
                        current = new BasisSet
                        {
                            ID = int.Parse(records[0]),
                            Code = records[1],
                            Name = records[2],
                            GmsInput = records[3]
                        };
                        retval.Add(current);
                    }
                }
            }
            return retval;
        }
    }
}
