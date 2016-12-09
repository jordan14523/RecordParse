using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordParse.Shared.Model;

namespace RecordParse.Shared.Interfaces
{
    public interface IParser<T>
    {
        List<T> Parse(List<string> linesToParse);
        T Parse(string line);
    }
}
