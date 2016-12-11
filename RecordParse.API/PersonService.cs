using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecordParse.API.Models;
using RecordParse.Shared;
using RecordParse.Shared.Interfaces;
using RecordParse.Shared.Model;

namespace RecordParse.API
{
    public class PersonService : IPersonService
    {
        private List<Person> _data = new List<Person>()
        {
            new Person() { DateOfBirth = DateTime.Now.AddDays(-1), LastName = "Castillo", FirstName = "Jordan", Gender = GenderEnum.Male, FavoriteColor = "blue"},
            new Person() { DateOfBirth = DateTime.Now, LastName = "Philbrick", FirstName = "Deidrea", Gender = GenderEnum.Female, FavoriteColor = "green"},
            new Person() { DateOfBirth = DateTime.Now.AddDays(-2), LastName = "Smith", FirstName = "John", Gender = GenderEnum.Male, FavoriteColor = "red"}
        };

        private readonly IPersonSorter _sorter;


        public PersonService(IPersonSorter sorter)
        {
            _sorter = sorter;
        }

        public List<PersonDto> GetByDateGender()
        {
            return MapToDto(_sorter.SortByGenderThenLastName(_data));
        }

        public List<PersonDto> GetByName()
        {
            return MapToDto(_sorter.SortByLastNameDesc(_data));
        }

        public List<PersonDto> GetByBirthdate()
        {
            return MapToDto(_sorter.SortByBirthdateAscending(_data));
        }

        public PersonDto MapToDto(Person person)
        {
            return new PersonDto
            {
                LastName = person.LastName,
                FirstName = person.FirstName,
                FavoriteColor = person.FavoriteColor,
                DateOfBirth = person.FormattedDobString(),
                Gender = person.GetGenderString()
            };
        }
        public List<PersonDto> MapToDto(List<Person> people)
        {
            return people.Select(x => MapToDto(x)).ToList();
        }

        public Person MapFromDto(PersonDto personDto)
        {
            throw new NotImplementedException();
        }

        public void Validate(PersonDto person)
        {
            throw new NotImplementedException();
        }
    }
}