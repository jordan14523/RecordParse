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
    public class UT_BaseParser
    {
        [Test]
        public void Parse_CallsSingleParse()
        {
            //arrange
            var input = new List<string>() {"test", "test"};
            var serializer = Substitute.For<ISerializer<object>>();
            var parser = Substitute.ForPartsOf<BaseParser<object>>(serializer);
            parser.When(x => x.Parse(Arg.Any<string>())).DoNotCallBase();

            //act
            parser.Parse(input);

            //assert
            parser.Received(2).Parse(Arg.Any<string>());
        }
    }
}
