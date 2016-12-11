using System.Collections.Generic;
using RecordParse.API.Models;
using RecordParse.Shared.Model;

namespace RecordParse.API
{
    public interface IPersonService
    {
        List<PersonDto> GetByDateGender();
        List<PersonDto> GetByName();
        List<PersonDto> GetByBirthdate();
        PersonDto MapToDto(Person person);
        List<PersonDto> MapToDto(List<Person> people);
        PersonDto Save(string input);
    }
}