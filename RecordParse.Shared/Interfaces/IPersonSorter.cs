using System.Collections.Generic;
using RecordParse.Shared.Model;

namespace RecordParse.Shared.Interfaces
{
    public interface IPersonSorter
    {
        List<Person> SortByGenderThenLastName(List<Person> people);
        List<Person> SortByBirthdateAscending(List<Person> people);
        List<Person> SortByLastNameDesc(List<Person> people);
    }
}