using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordParse.Shared.Interfaces;

namespace RecordParse.Shared.Parsers
{
    public class ParserFactory<T> : IParserFactory<T>
    {
        private readonly ISerializer<T> _serializer;

        public ParserFactory(ISerializer<T> serializer)
        {
            _serializer = serializer;
        }
            
        public IParser<T> GetParserFor(ParserEnum type)
        {
            switch (type)
            {
                case ParserEnum.Pipe:
                    return new PipeParser<T>(_serializer);
                case ParserEnum.Comma:
                    return new CommaParser<T>(_serializer);
                default:
                    return new SpaceParser<T>(_serializer);
            }
        }
    }
}
