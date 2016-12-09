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
    public class UT_CommaParser
    {
        [Test]
        public void Parser_Valid_SplitsInputOnComma()
        {
            //arrange
            var input = "test, test";
            var serializer = Substitute.For<ISerializer<object>>();
            var parser = new CommaParser<object>(serializer);

            //act
            parser.Parse(input);

            //assert
            serializer.Received().Serialize(Arg.Is<List<string>>(x => !x.Any(y => y.Contains(", "))));
            serializer.Received().Serialize(Arg.Is<List<string>>(x => x.Count == 2));
        }

    }
}
