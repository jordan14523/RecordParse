using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RecordParse.Shared.Interfaces;
using RecordParse.Shared.Model;

namespace RecordParse.Shared.Parsers
{
    public class PipeParser<T> : IParser<T>
    {
        private readonly ISerializer<T> _serializer;

        public PipeParser(ISerializer<T> serializer)
        {
            _serializer = serializer;
        }

        public List<T> Parse(List<string> linesToParse)
        {
            return linesToParse.Select(line => Parse(line)).ToList();
        }

        public T Parse(string line)
        {
            var items = line.Split(new[] { " | " }, StringSplitOptions.None).ToList();

            return _serializer.Serialize(items);
        }
    }
}
