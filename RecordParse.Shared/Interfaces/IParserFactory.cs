using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordParse.Shared.Parsers;

namespace RecordParse.Shared.Interfaces
{
    public interface IParserFactory<T>
    {
        IParser<T> GetParserFor(ParserEnum type);
    }
}
