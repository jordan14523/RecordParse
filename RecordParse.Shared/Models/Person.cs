using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordParse.Shared.Model
{
    public class Person
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public GenderEnum Gender { get; set; }
        public string FavoriteColor { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string GetGenderString()
        {
            switch (Gender)
            {
                case GenderEnum.Female:
                    return "Female";
                case GenderEnum.Male:
                    return "Male";
                default:
                    throw new Exception("Unexpected Gender Value.");
            }
        }

        public string FormattedDobString()
        {
            return DateOfBirth.ToString("M/d/yyyy");
        }
    }
}
