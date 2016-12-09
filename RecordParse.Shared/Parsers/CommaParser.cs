using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordParse.Shared.Interfaces;

namespace RecordParse.Shared.Parsers
{
    public class CommaParser<T> : BaseParser<T>
    {
        public CommaParser(ISerializer<T> serializer) : base(serializer) { }

        public override T Parse(string line)
        {
            var items = line.Split(new[] { ", " }, StringSplitOptions.None).ToList();

            return _serializer.Serialize(items);
        }
    }
}
