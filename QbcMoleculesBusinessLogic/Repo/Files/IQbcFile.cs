﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Repo.Files
{
    public interface IQbcFile
    {
        string ReadText(string path);

        bool FileExists(string path);

        List<string> ReadLines(string path);
        void WriteText(string path, string content);
        List<string> FindFiles(string path, string pattern);
        List<string> FindDirectories(string path, string pattern);
    }
}
