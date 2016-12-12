using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using RecordParse.Shared.Model;

namespace RecordParse.API.Models
{

    [ExcludeFromCodeCoverage]
    public class PersonDto
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string FavoriteColor { get; set; }
        public string DateOfBirth { get; set; }
    }
}