using System.Collections.Generic;
using System.Net.Http.Headers;
using NSubstitute;
using NUnit.Framework;
using RecordParse.CLI;
using RecordParse.CLI.Interfaces;

namespace RegexParse.Tests.CLI
{
    [TestFixture]
    public class UT_ArgumentValidation
    {
        private IFile _file;

        [SetUp]
        public void SetUp()
        {
            _file = Substitute.For<IFile>();
        }

        [Test]
        [TestCase("-h", true)]
        [TestCase("-H", true)]
        [TestCase("--help", true)]
        [TestCase("--HELP", true)]
        [TestCase("blah", false)]
        public void IsHelpCommand_AllSingleArgCases(string input, bool expected)
        {
             //arrange
             var argValidator = GetArgValidator();

            //act
            var result = argValidator.IsHelpCommand(new [] {input});

            //assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void IsHelpCommand_MultipleArgs_ReturnsFalse()
        {
            //arrange
            var args = new[] {"", ""};
            var argValidator = GetArgValidator();

            //act
            var result = argValidator.IsHelpCommand(args);

            //assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void CheckArgCount_ExpectedCount_ReturnsTrue()
        {
            //arrange
            var args = new[] {"", "", ""};
            var validResults = new List<string>();
            var validator = GetArgValidator();

            //act
            var result = validator.CheckArgCount(args, validResults);

            //assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(0, validResults.Count);
        }

        [Test]
        public void CheckArgCount_UnExpectedCount_ReturnsFalse()
        {
            //arrange
            var args = new[] { "", ""};
            var validResults = new List<string>();
            var validator = GetArgValidator();

            //act
            var result = validator.CheckArgCount(args, validResults);

            //assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(1, validResults.Count);
        }

        [Test]
        public void CheckArgPaths_ValidPaths_ReturnsTrue()
        {
            //arrange
            var args = new string[] {};
            var validResults = new List<string>();
            _file.Exists(Arg.Any<string>()).ReturnsForAnyArgs(true);
            var validator = GetArgValidator();

            //act
            var result = validator.CheckArgPaths(args, validResults);

            //assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(0, validResults.Count);
        }

        [Test]
        public void CheckArgPaths_InValidPaths_ReturnsFalse()
        {
            //arrange
            var args = new [] { "", "", "" };
            var validResults = new List<string>();
            _file.Exists(Arg.Any<string>()).ReturnsForAnyArgs(false);
            var validator = GetArgValidator();

            //act
            var result = validator.CheckArgPaths(args, validResults);

            //assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(3, validResults.Count);
        }


        public ArgumentValidator GetArgValidator()
        {
            return new ArgumentValidator(_file);
        }

    }
}
