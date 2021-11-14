using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Repo.Files
{
    public class QbcFile : IQbcFile
    {
        public string ReadText(string path)
        {
            if ( File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                return string.Empty;
            }
        }

        public void WriteText(string path, string content)
        {
            if ( File.Exists(path))
            {
                File.WriteAllText(path, content);
            }
        }
    }
}
