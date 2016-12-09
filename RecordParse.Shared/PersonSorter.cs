using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordParse.Shared.Interfaces;
using RecordParse.Shared.Model;

namespace RecordParse.Shared
{
    public class PersonSorter : IPersonSorter
    {
        public List<Person> SortByGenderThenLastName(List<Person> people)
        {
            return people.OrderBy(x => x.Gender).ThenBy(y => y.LastName).ToList();
        }

        public List<Person> SortByBirthdateAscending(List<Person> people)
        {
            return people.OrderBy(x => x.DateOfBirth).ToList();
        }

        public List<Person> SortByLastNameDesc(List<Person> people)
        {
            return people.OrderByDescending(x => x.LastName).ToList();
        }
    }
}
