using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Repo.Resources
{
    public class QbcResource : IQbcResource
    {
        public string ReadResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string retval = string.Empty;
            if (assembly != null)
            {
                using Stream? stream = assembly.GetManifestResourceStream(resourceName);
                if (stream != null)
                {
                    using StreamReader reader = new(stream);
                    retval = reader.ReadToEnd();
                }
            }
            return retval;
        }
    }
}
