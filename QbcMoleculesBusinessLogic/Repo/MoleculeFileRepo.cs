using QbcMoleculesBusinessLogic.Data.Molecules;
using QbcMoleculesBusinessLogic.Repo.Files;
using QbcMoleculesBusinessLogic.Repo.Formatter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Repo
{
    public class MoleculeFileRepo : IMoleculeFileRepo
    {

        #region dependencies

        private IQbcFile QbcFile { get; }

        private IQbcFormatter QbcFormatter { get; }

        #endregion

        public MoleculeFileRepo(IQbcFormatter qbcFormatter, IQbcFile qbcFile)
        {
            QbcFormatter = qbcFormatter;
            QbcFile = qbcFile;
        }

        public Molecule? ReadFromFile(string path)
        {
            string fileData = QbcFile.ReadText(path);
            Molecule? retval = null;
            if ( !String.IsNullOrEmpty(fileData) )
            {
                retval = QbcFormatter.DeserializeObjectFromString<Molecule>(fileData);
            }
            return retval;
        }

        public void WriteToFile(Molecule? molecule, string path)
        {
            if (molecule != null)
            {
                QbcFile.WriteText($"{Path.Combine( path, molecule.NameInfo)}.json", QbcFormatter.SerializeObjectToString(molecule));
            }
        }

        public bool MoleculeExists(Molecule? molecule, string path)
        {
            return QbcFile.FileExists($"{Path.Combine(path, molecule?.NameInfo)}.json");
        }
    }
}
