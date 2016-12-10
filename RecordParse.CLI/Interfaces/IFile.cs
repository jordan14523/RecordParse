using System.Collections.Generic;

namespace RecordParse.CLI.Interfaces
{
    public interface IFile
    {
        IEnumerable<string> ReadAllLines(string path);
        bool Exists(string path);
    }
}
