using System;
using System.Collections.Generic;
using NUnit.Framework;
using RecordParse.Shared;
using RecordParse.Shared.Model;

namespace RegexParse.Tests.Shared
{
    [TestFixture]
    public class UT_PersonSorter
    {
        [Test]
        public void SortByGenderThenLastName_SortsExpected()
        {
            //arrange
            var peopleForTest = new List<Person>()
            {
                new Person() { Gender =  GenderEnum.Male, LastName = "B"},
                new Person() { Gender =  GenderEnum.Male, LastName = "A"},
                new Person() { Gender =  GenderEnum.Female, LastName = "B"},
                new Person() { Gender =  GenderEnum.Female, LastName = "A"}
            };
            var sorter = new PersonSorter();

            //act
            var results = sorter.SortByGenderThenLastName(peopleForTest);

            //assert
            Assert.IsTrue(results[0].Gender == GenderEnum.Female && results[0].LastName == "A");
            Assert.IsTrue(results[1].Gender == GenderEnum.Female && results[1].LastName == "B");
            Assert.IsTrue(results[2].Gender == GenderEnum.Male && results[2].LastName == "A");
            Assert.IsTrue(results[3].Gender == GenderEnum.Male && results[3].LastName == "B");
        }

        [Test]
        public void SortByBirthdateAscending_SortsExpected()
        {
            //arrange
            var people = new List<Person>()
            {
                new Person() { DateOfBirth = DateTime.Now },
                new Person() { DateOfBirth = DateTime.Now.AddDays(-1) }
            };
            var sorter = new PersonSorter();
            
            //act
            var results = sorter.SortByBirthdateAscending(people);

            //assert
            Assert.IsTrue(results[0].DateOfBirth < results[1].DateOfBirth);
        }

        [Test]
        public void SortByLastNameDesc_SortsExpected()
        {
            //arrange
            var people = new List<Person>()
            {
                new Person() { LastName = "A" },
                new Person() { LastName = "B" }
            };
            var sorter = new PersonSorter();

            //act
            var results = sorter.SortByLastNameDesc(people);

            //assert
            Assert.AreEqual("B", results[0].LastName);
            Assert.AreEqual("A", results[1].LastName);
        }
    }
}
