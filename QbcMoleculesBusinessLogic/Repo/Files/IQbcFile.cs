using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Repo.Files
{
    public interface IQbcFile
    {
        string ReadText(string path);

        void WriteText(string path, string content);

    }
}
