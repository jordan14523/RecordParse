using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using NSubstitute;
using NUnit.Framework;
using RecordParse.Shared.Interfaces;
using RecordParse.Shared.Parsers;

namespace RegexParse.Tests.Shared
{
    [TestFixture]
    public class UT_ParserFactory
    {
        private ISerializer<object> _serializer;

        [SetUp]
        public void SetUp()
        {
            _serializer = Substitute.For<ISerializer<object>>();
        }

        [Test]
        [TestCase(ParserEnum.Comma, typeof(CommaParser<object>))]
        [TestCase(ParserEnum.Pipe, typeof(PipeParser<object>))]
        [TestCase(ParserEnum.Space, typeof(SpaceParser<object>))]
        public void GetParerFor(ParserEnum parserType, Type expected)
        {
            //arrange
            var parserFactory = new ParserFactory<object>(_serializer);

            //act
            var result = parserFactory.GetParserFor(parserType);

            //assert
            Assert.IsInstanceOf(expected, result);
        }
    }
}
