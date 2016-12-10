using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordParse.CLI.Interfaces;

namespace RecordParse.CLI
{
    public class ArgumentValidator : IArgumentValidator
    {
        private readonly IFile _file;

        public ArgumentValidator(IFile file)
        {
            _file = file;
        }

        public void Validate(string[] args)
        {
            var validResults = new List<string>();

            if (!CheckArgCount(args, validResults) || !CheckArgPaths(args, validResults))
            {
                throw new ArgumentValidationException("Incorrect arguments given.")
                {
                    ValidationResults = validResults
                };
            }
        }

        public virtual bool IsHelpCommand(string[] args)
        {
            return args.Length == 1 
                        && (string.Compare(args[0], "-h", StringComparison.CurrentCultureIgnoreCase) == 0
                            || string.Compare(args[0], "--help", StringComparison.CurrentCultureIgnoreCase) == 0);
        }

        public virtual bool CheckArgCount(string[] args, List<string> validResults)
        {
            if (args.Length == 3)
            {
                return true;
            };
            validResults.Add($"Not enough file arguments. Expected: 3, Actual: {args.Length}");
            return false;
        }

        public virtual bool CheckArgPaths(string[] args, List<string> validResults)
        {
            foreach (var arg in args)
            {
                if(!_file.Exists(arg))
                {
                    validResults.Add($"Could not find file {arg}.");  
                }
            }
            return validResults.Count == 0;
        }
    }
}
