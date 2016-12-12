using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RecordParse.Shared;
using RecordParse.Shared.Model;

namespace RegexParse.Tests.Shared
{
    [TestFixture]
    public class UT_Person
    {
        [Test]
        [TestCase(GenderEnum.Male, "Male")]
        [TestCase(GenderEnum.Female, "Female")]
        public void GetGenderString_ReturnsExpectedString(GenderEnum gender, string expected)
        {
            //arrange
            var person = new Person() { Gender = gender };

            //act
            var result = person.GetGenderString();

            //assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void FormattedDobString_ReturnsCorrectFormat()
        {
            //arrange
            var date = "1/1/2016";
            var person = new Person() {DateOfBirth = DateTime.Parse(date)};

            //act
            var result = person.FormattedDobString();

            //assert
            Assert.AreEqual(date, result);
        }
    }
}
