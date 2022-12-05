namespace QbcMoleculesBusinessLogic.Repo.Files
{
    public class QbcFile : IQbcFile
    {
        public List<string> FindDirectories(string path, string pattern)
        {
            return new List<string>(Directory.EnumerateDirectories(path, pattern, SearchOption.TopDirectoryOnly));
        }

        public List<string> FindFiles(string path, string pattern)
        {
            return new List<string>(Directory.EnumerateFiles(path,pattern, SearchOption.TopDirectoryOnly));
        }

        public bool PathExists(string path)
        {
            return Path.Exists(path);  
        }

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

        public List<string> ReadLines(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllLines(path).ToList();
            }
            else
            {
                return new List<string>();
            }
        }


        public void WriteText(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
