using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using RecordParse.API;
using RecordParse.API.Models;
using RecordParse.Shared.Interfaces;
using RecordParse.Shared.Model;
using RecordParse.Shared.Parsers;

namespace RegexParse.Tests.API
{
    [TestFixture]
    public class UT_PersonService
    {
        private IPersonSorter _sorter;
        private IParserFactory<Person> _parserFactory;

        [SetUp]
        public void SetUp()
        {
            _sorter = Substitute.For<IPersonSorter>();
            _parserFactory = Substitute.For<IParserFactory<Person>>();
        }

        [Test]
        [TestCase("|", ParserEnum.Pipe)]
        [TestCase(",", ParserEnum.Comma)]
        [TestCase(" ", ParserEnum.Space)]
        public void DetectDelimiter_DetectsAllDelimeters(string input, ParserEnum type)
        {
            //arrange
            var personService = GetService();

            //act
            var result = personService.DetectDelimeter(input);

            //assert
            Assert.AreEqual(type, result);
        }

        [Test]
        public void GetByBirthdate_CallsSorter()
        {
            //arrange
            var sorted = new List<Person>();
            _sorter.SortByBirthdateAscending(null).ReturnsForAnyArgs(sorted);
            var personService = GetMockableService();
            personService.WhenForAnyArgs(x => x.MapToDto(Arg.Any<List<Person>>())).DoNotCallBase();

            //act
            var result = personService.GetByBirthdate();

            //assert
            _sorter.Received().SortByBirthdateAscending(Arg.Any<List<Person>>());
            personService.Received().MapToDto(sorted);
        }

        [Test]
        public void GetByGender_CallsSorter()
        {
            //arrange
            var sorted = new List<Person>();
            _sorter.SortByGenderThenLastName(null).ReturnsForAnyArgs(sorted);
            var personService = GetMockableService();
            personService.WhenForAnyArgs(x => x.MapToDto(Arg.Any<List<Person>>())).DoNotCallBase();

            //act
            var result = personService.GetByGender();

            //assert
            _sorter.Received().SortByGenderThenLastName(Arg.Any<List<Person>>());
            personService.Received().MapToDto(sorted);
        }

        [Test]
        public void GetByName_CallsSorter()
        {
            //arrange
            var sorted = new List<Person>();
            _sorter.SortByLastNameDesc(null).ReturnsForAnyArgs(sorted);
            var personService = GetMockableService();
            personService.WhenForAnyArgs(x => x.MapToDto(Arg.Any<List<Person>>())).DoNotCallBase();

            //act
            var result = personService.GetByName();

            //assert
            _sorter.Received().SortByLastNameDesc(Arg.Any<List<Person>>());
            personService.Received().MapToDto(sorted);
        }

        [Test]
        public void Save_AddsPerson()
        {
            //arrange
            var type = ParserEnum.Pipe;
            var input = "";
            var person = new Person();
            var personDto = new PersonDto();
            var parser = Substitute.For<IParser<Person>>();
            parser.Parse(input).Returns(person);
            _parserFactory.GetParserFor(type).Returns(parser);

            var personService = GetMockableService();
            personService.WhenForAnyArgs(x => x.DetectDelimeter(Arg.Any<string>())).DoNotCallBase();
            personService.WhenForAnyArgs(x => x.MapToDto(Arg.Any<Person>())).DoNotCallBase();
            personService.MapToDto(person).Returns(personDto);
            personService.DetectDelimeter(input).Returns(type);

            //act
            var result = personService.Save(input);

            //assert
            Assert.IsTrue(personService.GetData().Contains(person));
            Assert.AreSame(personDto, result);
        }

        public PersonService GetService()
        {
            return new PersonService(_sorter, _parserFactory);
        }

        public PersonService GetMockableService()
        {
            return Substitute.ForPartsOf<PersonService>(_sorter, _parserFactory);
        }
    }
}
