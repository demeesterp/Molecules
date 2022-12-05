namespace QbcMoleculesBusinessLogic.Repo.Files
{
    public interface IQbcFile
    {
        string ReadText(string path);
        bool PathExists(string path);
        List<string> ReadLines(string path);
        void WriteText(string path, string content);
        List<string> FindFiles(string path, string pattern);
        List<string> FindDirectories(string path, string pattern);
    }
}
