using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using RecordParse.Shared.Interfaces;
using RecordParse.Shared.Parsers;

namespace RegexParse.Tests.Shared
{
    [TestFixture]
    public class UT_PipeParser
    {
        [Test]
        public void Parse_Valid_SplitsInputOnPipe()
        {
            //arrange
            var input = "test | test";
            var serializer = Substitute.For<ISerializer<object>>();
            var pipeParser = new PipeParser<object>(serializer);

            //act
            var result = pipeParser.Parse(input);

            //assert
            serializer.Received().Serialize(Arg.Is<List<string>>(x => !x.Any(y => y.Contains(" | "))));
            serializer.Received().Serialize(Arg.Is<List<string>>(x => x.Count == 2));
        }
    }
}
