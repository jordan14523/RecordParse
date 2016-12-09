using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using RecordParse.Shared.Interfaces;
using RecordParse.Shared.Model;

namespace RecordParse.Shared
{
    public class PersonSerializer : ISerializer<Person>
    {
        public Person Serialize(List<string> items)
        {
            if (items.Count() != 5)
            {
                throw new SerializerException("Items in line do not have enough members")
                {
                    Data = {{SerializerExceptionDataKeys.LineValues, items}}
                };
            }

            return new Person()
            {
                LastName = items[0],
                FirstName = items[1],
                Gender = ParseGender(items[2]),
                FavoriteColor = items[3],
                DateOfBirth = ParseDate(items[4])
            };
        }

        public virtual DateTime ParseDate(string date)
        {
            try
            {
                return DateTime.Parse(date);
            }
            catch (FormatException)
            {
                throw new SerializerException("Could not parse date of birth.")
                {
                    Data = { { SerializerExceptionDataKeys.DateOfBirth, date } }
                };
            }
        }

        public virtual GenderEnum ParseGender(string gender)
        {
            if (string.Compare(gender, "male", StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                return GenderEnum.Male;
            }

            if (string.Compare(gender, "female", StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                return GenderEnum.Female;
            }

            throw new SerializerException("Could not parse gender.")
            {
                Data = { { SerializerExceptionDataKeys.Gender, gender } }
            };
        }
    }
}
