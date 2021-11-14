using QbcMoleculesBusinessLogic.Data.DataFiles;

namespace QbcMoleculesBusinessLogic.Repo
{
    public interface IBasissetInfoRepo
    {
        List<BasisSet> GetBasisSetInfo();
    }
}
