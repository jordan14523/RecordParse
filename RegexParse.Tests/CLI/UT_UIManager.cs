using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using RecordParse.CLI;
using RecordParse.CLI.Interfaces;
using RecordParse.Shared;
using RecordParse.Shared.Interfaces;
using RecordParse.Shared.Model;
using RecordParse.Shared.Parsers;

namespace RegexParse.Tests.CLI
{
    [TestFixture]
    public class UT_UIManager
    {
        private IPersonSorter _personSorter;
        private IParserFactory<Person> _parserFactory;
        private IArgumentValidator _argValidator;
        private IFile _file;

        [SetUp]
        public void Test()
        {
            _personSorter = Substitute.For<IPersonSorter>();
            _parserFactory = Substitute.For<IParserFactory<Person>>();
            _argValidator = Substitute.For<IArgumentValidator>();
            _file = Substitute.For<IFile>();
        }

        [Test]
        public void Start_IsHelpCommand_CallsDisplayHelp()
        {
            //arrange
            var args = new[] { "" };
            _argValidator.IsHelpCommand(args).Returns(true);
            var manager = GetMockableUIManager();

            //mock virtuals
            manager.WhenForAnyArgs(x=> x.DisplayHelp()).DoNotCallBase();
            manager.WhenForAnyArgs(x => x.ReadFiles(null)).DoNotCallBase();
            manager.WhenForAnyArgs(x => x.DisplaySorted(null)).DoNotCallBase();
            manager.WhenForAnyArgs(x => x.DisplayArgException(null)).DoNotCallBase();

            //act
            manager.Start(args);

            //assert
            manager.Received().DisplayHelp();
            manager.DidNotReceiveWithAnyArgs().ReadFiles(null);
            manager.DidNotReceiveWithAnyArgs().DisplaySorted(null);
            manager.DidNotReceiveWithAnyArgs().DisplayArgException(null);
        }
        
        [Test]
        public void Start_ArgsInvalid_CallsDisplayArgException()
        {
            //arrange
            var args = new[] {""};
            var ex = new ArgumentValidationException("");
            _argValidator.When(x => x.Validate(args)).Do(x => { throw ex;  });
            var manager = GetMockableUIManager();

            //mock virtuals
            manager.WhenForAnyArgs(x => x.DisplayHelp()).DoNotCallBase();
            manager.WhenForAnyArgs(x => x.ReadFiles(null)).DoNotCallBase();
            manager.WhenForAnyArgs(x => x.DisplaySorted(null)).DoNotCallBase();
            manager.WhenForAnyArgs(x => x.DisplayArgException(null)).DoNotCallBase();

            //act
            manager.Start(args);

            //assert
            manager.Received().DisplayArgException(ex);
        }

        [Test]
        public void Start_ArgsValid_CallsReadFiles()
        {
            //arrange
            var args = new[] { "" };
            var manager = GetMockableUIManager();

            //mock virtuals
            manager.WhenForAnyArgs(x => x.DisplayHelp()).DoNotCallBase();
            manager.WhenForAnyArgs(x => x.ReadFiles(null)).DoNotCallBase();
            manager.WhenForAnyArgs(x => x.DisplaySorted(null)).DoNotCallBase();
            manager.WhenForAnyArgs(x => x.DisplayArgException(null)).DoNotCallBase();

            //act
            manager.Start(args);

            //assert
            manager.Received().ReadFiles(args);
        }

        [Test]
        public void Start_ArgsValid_CallsDisplaySorted()
        {
            //arrange
            var args = new[] { "" };
            var people = new List<Person>();
            var manager = GetMockableUIManager();

            //mock virtuals
            manager.WhenForAnyArgs(x => x.DisplayHelp()).DoNotCallBase();
            manager.WhenForAnyArgs(x => x.ReadFiles(null)).DoNotCallBase();
            manager.WhenForAnyArgs(x => x.DisplaySorted(null)).DoNotCallBase();
            manager.WhenForAnyArgs(x => x.DisplayArgException(null)).DoNotCallBase();

            manager.ReadFiles(args).Returns(people);

            //act
            manager.Start(args);

            //assert
            manager.Received().DisplaySorted(people);
        }

        [Test]
        public void BuildDisplayLineForPerson_BuildsPaddedLine()
        {
            //arrange
            var person = new Person() { DateOfBirth = DateTime.Now, FavoriteColor = "", LastName = "", FirstName = "", Gender = GenderEnum.Female};
            var manager = GetUIManager();

            //act
            var result = manager.BuildDisplayLineForPerson(person);

            //assert
            Assert.AreEqual(76, result.Length);
        }

        [Test]
        public void ReadFiles_CallsFileReaderForAllTypes()
        {
            //arrange
            var args = new[] { "1", "2", "3"};
            var mockParser = Substitute.For<IParser<Person>>();
            mockParser.Parse(Arg.Any<List<string>>()).ReturnsForAnyArgs(new List<Person>());
            _parserFactory.GetParserFor(Arg.Any<ParserEnum>()).ReturnsForAnyArgs(mockParser);
            var manager = GetUIManager();


            //act
            manager.ReadFiles(args);

            //assert
            _file.Received().ReadAllLines(args[0]);
            _file.Received().ReadAllLines(args[1]);
            _file.Received().ReadAllLines(args[2]);
        }
        [Test]
        public void ReadFiles_GetsParserForAllTypes()
        {
            //arrange
            var args = new[] { "", "", "" };
            var mockParser = Substitute.For<IParser<Person>>();
            mockParser.Parse(Arg.Any<List<string>>()).ReturnsForAnyArgs(new List<Person>());
            _parserFactory.GetParserFor(Arg.Any<ParserEnum>()).ReturnsForAnyArgs(mockParser); //Any arg
            var manager = GetUIManager();


            //act
            manager.ReadFiles(args);

            //assert
            _parserFactory.Received().GetParserFor(ParserEnum.Pipe);
            _parserFactory.Received().GetParserFor(ParserEnum.Comma);
            _parserFactory.Received().GetParserFor(ParserEnum.Pipe);
        }

        public UIManager GetUIManager()
        {
            return new UIManager(_personSorter, _argValidator, _parserFactory, _file);
        }

        public UIManager GetMockableUIManager()
        {
            return Substitute.ForPartsOf<UIManager>(_personSorter, _argValidator, _parserFactory, _file);
        }

    }
}
