using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Data.ProcessCommands
{

    public enum QbcCmdName
    {
        dummy = 0,
        procescalculation = 1
    }


    public class QbcCmd
    {

        public QbcCmd()
        {
            Name = QbcCmdName.dummy;
        }

        public QbcCmd(string name)
        {
            Name = Convert(name);
        }


        public static QbcCmdName Convert(string name)
        {
            return Enum.TryParse(name, true, out QbcCmdName cmdName) ? cmdName : QbcCmdName.dummy;
        }

        public QbcCmdName Name { get; }

        public List<QbcCmdParameter> Parameters { get; set; }


    }
}
