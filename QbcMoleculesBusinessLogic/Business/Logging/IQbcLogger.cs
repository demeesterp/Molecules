using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Business.Logging
{
    public interface IQbcLogger
    {
        void LogInformation(string message);

        void LogWarning(Exception e, string message);

        void LogWarning(string message);

        void LogError(Exception e, string message);

        void LogCritical(Exception e, string message);
    }
}
