using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecordParse.API.Models;
using RecordParse.Shared;
using RecordParse.Shared.Interfaces;
using RecordParse.Shared.Model;
using RecordParse.Shared.Parsers;

namespace RecordParse.API
{
    public class PersonService : IPersonService
    {
        private List<Person> _data = new List<Person>();
        private readonly IPersonSorter _sorter;
        private readonly IParserFactory<Person> _parserFactory;

        public PersonService(IPersonSorter sorter, IParserFactory<Person> parserFactory)
        {
            _sorter = sorter;
            _parserFactory = parserFactory;
        }

        public IReadOnlyList<Person> GetData()
        {
            return _data;
        }

        public PersonDto Save(string input)
        {
            var type = DetectDelimeter(input);
            var parser = _parserFactory.GetParserFor(type);
            var person = parser.Parse(input);
            _data.Add(person);
            return MapToDto(person);
        }

        public List<PersonDto> GetByGender()
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

        public virtual PersonDto MapToDto(Person person)
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

        public virtual List<PersonDto> MapToDto(List<Person> people)
        {
            return people.Select(x => MapToDto(x)).ToList();
        }

        public virtual ParserEnum DetectDelimeter(string input)
        {
            if (input.Contains("|"))
            {
                return ParserEnum.Pipe;
            }

            if (input.Contains(","))
            {
                return ParserEnum.Comma;
            }

            return ParserEnum.Space;
        }
    }
}