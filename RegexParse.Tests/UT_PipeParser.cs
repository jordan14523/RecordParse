using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using RecordParse.Shared.Interfaces;
using RecordParse.Shared.Parsers;

namespace RegexParse.Tests
{
    [TestFixture]
    public class UT_PipeParser
    {
        public void Parse_Valid_SplitsInputOnPipe()
        {
            //arrange
            var input = "test | test";
            var serializer = Substitute.For<ISerializer<object>>();
            var pipeParser = new PipeParser<object>(serializer);

            //act
            var result = pipeParser.Parse(input);

            //assert
            pipeParser.Received().Parse(Arg.Is<List<string>>(x => !x.Any(y => y.Contains(" | "))));
            pipeParser.Received().Parse(Arg.Is<List<string>>(x => x.Count == 2));
        }
    }
}
