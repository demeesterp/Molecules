using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Business.Logging
{
    public class QbcLogger : IQbcLogger
    {
        public void LogCritical(Exception e, string message)
        {
            Console.Error.WriteLine($"Critical : {message} Exception {WriteException(e)}");
        }

        public void LogError(Exception e, string message)
        {
            Console.Error.WriteLine($"Error : {message} Exception {WriteException(e)}");
        }

        public void LogInformation(string message)
        {
            Console.Error.WriteLine($"Information : {message}");
        }

        public void LogWarning(Exception e, string message)
        {
            Console.Error.WriteLine($"Warning : {message} Exception {WriteException(e)}");
        }

        public void LogWarning(string message)
        {
            Console.Error.WriteLine($"Warning : {message}");
        }

        #region helpers

        private static string WriteException(Exception e)
        {
            StringBuilder str = new();
            Exception? current = e;
            do
            {
                str.AppendLine(e.Message);
                str.AppendLine(e.StackTrace);
                current = current.InnerException;
            }
            while (current != null);
            return str.ToString();
        }


        #endregion
    }
}
