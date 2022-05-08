using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Data.ProcessCommands
{
    public class QbcCmdInterpreter
    {
        public static List<QbcCmd> BuildCmd(string[] cmdargs)
        {
            List<QbcCmd> retval = new ();
            foreach (string cmd in cmdargs)
            {
                if ( cmd.StartsWith('-'))
                {
                    retval.Add(new QbcCmd(cmd[1..]));
                }
                else if (retval.Count > 0)
                {
                    string[] result = cmd.Split('=', StringSplitOptions.RemoveEmptyEntries);
                    if ( result.Length > 1 )
                    {
                        retval.Last().Parameters.Add(new QbcCmdParameter()
                        {
                            Name = result[0],
                            Value = result[1]
                        });
                    }
                    else
                    {
                        retval.Last().Parameters.Add(new QbcCmdParameter()
                        {
                            Name = result[0],
                            Value = String.Empty
                        });
                    }
                }
            }
            return retval;
        }

    }
}
