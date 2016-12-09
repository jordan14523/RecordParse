using System;
using System.Collections.Generic;
using System.Linq;
using RecordParse.Shared.Interfaces;

namespace RecordParse.Shared.Parsers
{
    public abstract class BaseParser<T> : IParser<T>
    {
        protected ISerializer<T> _serializer;

        protected BaseParser(ISerializer<T> serializer)
        {
            _serializer = serializer;
        }

        public List<T> Parse(List<string> linesToParse)
        {
            return linesToParse.Select(line => Parse(line)).ToList();
        }

        public abstract T Parse(string line);
    }
}