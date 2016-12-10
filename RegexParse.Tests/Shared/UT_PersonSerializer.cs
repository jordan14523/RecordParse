using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using RecordParse.Shared;

namespace RegexParse.Tests.Shared
{
    [TestFixture]
    public class UT_PersonSerializer
    {
        [Test]
        public void ParseDate_DateIsValid_ReturnsDateTime()
        {
            //arrange
            var date = "01/01/2016";
            var serializer = new PersonSerializer();

            //act
            var result = serializer.ParseDate(date);

            //assert
            Assert.IsTrue(result.Day == 1);
            Assert.IsTrue(result.Month == 1);
            Assert.IsTrue(result.Year == 2016);
        }

        [Test]
        public void ParseDate_InvalidDate_ThrowsSerializerException()
        {
            //arrange
            var badDate = "blah";
            var serializer  = new PersonSerializer();
            
            //act and assert
            var ex = Assert.Throws<SerializerException>(() => serializer.ParseDate(badDate));
            Assert.AreEqual(badDate, ex.Data[SerializerExceptionDataKeys.DateOfBirth]);
        }

        [Test]
        [TestCase("male", GenderEnum.Male)]
        [TestCase("MALE", GenderEnum.Male)]
        [TestCase("female", GenderEnum.Female)]
        [TestCase("FEMALE", GenderEnum.Female)]
        public void ParseGender_ValidCases_ReturnsExpected(string inGender, GenderEnum expected)
        {
            //arrange
            var serializer = new PersonSerializer();

            //act
            var result = serializer.ParseGender(inGender);

            //assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Serialize_NotEnoughItems_ThrowsSerializerException()
        {
            //arrange
            var items = new List<string>() {"test"};
            var serializer = new PersonSerializer();
            
            //act and assert
            var ex = Assert.Throws<SerializerException>(() => serializer.Serialize(items));
            Assert.AreEqual(items, ex.Data[SerializerExceptionDataKeys.LineValues]);
        }

        [Test]
        public void Serialize_ValidInput_ReturnsPerson()
        {
            //arrange
            string lastName = "last", firstName = "first", gender = "female", favoriteColor= "green", birthday="01/01/2016";
            var items = new List<string>() { lastName, firstName, gender, favoriteColor, birthday };
            var mockedDate = DateTime.Now;
            var mockedGender = GenderEnum.Female;
            var serializer = Substitute.ForPartsOf<PersonSerializer>();
            serializer.WhenForAnyArgs(x => x.ParseDate(null)).DoNotCallBase(); //mock parse date method
            serializer.ParseDate(null).ReturnsForAnyArgs(mockedDate);
            serializer.WhenForAnyArgs(x => x.ParseGender(null)).DoNotCallBase(); //mock parse date method
            serializer.ParseGender(null).ReturnsForAnyArgs(mockedGender);

            //act
            var result = serializer.Serialize(items);

            //assert
            Assert.AreEqual(lastName, result.LastName);
            Assert.AreEqual(firstName, result.FirstName);
            Assert.AreEqual(mockedGender, result.Gender);
            Assert.AreEqual(favoriteColor, result.FavoriteColor);
            Assert.AreEqual(mockedDate, result.DateOfBirth);
        }
    }
}
