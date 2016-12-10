using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordParse.CLI
{
    public class ArgumentValidationException : Exception
    {
        public List<string> ValidationResults { get; set; }
        public ArgumentValidationException(string message) : base(message)
        {
        }
    }
}
