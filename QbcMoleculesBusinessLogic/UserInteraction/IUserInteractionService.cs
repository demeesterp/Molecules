using QbcMoleculesBusinessLogic.Data.DataFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.UserInteraction
{
    public interface IUserInteractionService
    {

        BasisSet? SelectBasisSet();

    }
}
