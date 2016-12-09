using System;
using System.Linq;
using RecordParse.Shared.Interfaces;

namespace RecordParse.Shared.Parsers
{
    public class PipeParser<T> : BaseParser<T>
    {
        public PipeParser(ISerializer<T> serializer) : base(serializer) { }

        public override T Parse(string line)
        {
            var items = line.Split(new[] { " | " }, StringSplitOptions.None).ToList();

            return _serializer.Serialize(items);
        }
    }
}
