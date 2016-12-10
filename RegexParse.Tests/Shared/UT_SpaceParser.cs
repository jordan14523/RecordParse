using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using RecordParse.Shared.Interfaces;
using RecordParse.Shared.Parsers;

namespace RegexParse.Tests.Shared
{
    [TestFixture]
    public class UT_SpaceParser
    {
        [Test]
        public void Parser_Valid_SplitsInputOnSpace()
        {
            //arrange
            var input = "test test";
            var serializer = Substitute.For<ISerializer<object>>();
            var parser = new SpaceParser<object>(serializer);

            //act
            parser.Parse(input);

            //assert
            serializer.Received().Serialize(Arg.Is<List<string>>(x => !x.Any(y => y.Contains(" "))));
            serializer.Received().Serialize(Arg.Is<List<string>>(x => x.Count == 2));
        }
    }
}
