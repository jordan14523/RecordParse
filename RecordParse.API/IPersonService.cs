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
        Person MapFromDto(PersonDto personDto);
        void Validate(PersonDto person);
        List<PersonDto> MapToDto(List<Person> people);
    }
}