using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordParse.CLI.Interfaces;

namespace RecordParse.CLI
{
    public class FileAdapter : IFile
    {
        public IEnumerable<string> ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}
