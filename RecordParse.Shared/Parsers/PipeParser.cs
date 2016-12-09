﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RecordParse.Shared.Interfaces;
using RecordParse.Shared.Model;

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
