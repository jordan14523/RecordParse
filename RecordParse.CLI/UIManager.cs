using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordParse.CLI.Interfaces;
using RecordParse.Shared.Interfaces;
using RecordParse.Shared.Model;
using RecordParse.Shared.Parsers;

namespace RecordParse.CLI
{
    public class UIManager : IUIManager
    {
        private readonly IPersonSorter _personSorter;
        private readonly IParserFactory<Person> _parserFactory;
        private readonly IArgumentValidator _argValidator;
        private readonly IFile _file;

        public UIManager(IPersonSorter personSorter, IArgumentValidator argValidator, IParserFactory<Person> parserFactory, IFile file)
        {
            _personSorter = personSorter;
            _argValidator = argValidator;
            _parserFactory = parserFactory;
            _file = file;
        }

        public void Start(string[] args)
        {
            if(_argValidator.IsHelpCommand(args))
            { 
                DisplayHelp();
            }
            else
            {
                try
                {
                    _argValidator.Validate(args);
                    var people = ReadFiles(args);
                    DisplaySorted(people);
                }
                catch (ArgumentValidationException ex)
                {
                    DisplayArgException(ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An exception occurred.");
                    Console.WriteLine($"Exception message: {ex.Message}");
                }
            }
        }

        public virtual void DisplaySorted(List<Person> people)
        {
            Console.WriteLine("Sorted by Gender and Last Name:" + Environment.NewLine);
            DisplayPeople(_personSorter.SortByGenderThenLastName(people));
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Sorted by Birthday Ascending:" + Environment.NewLine);
            DisplayPeople(_personSorter.SortByBirthdateAscending(people));
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Sorted by Last Name Descending:" + Environment.NewLine);
            DisplayPeople(_personSorter.SortByLastNameDesc(people));
        }

        public virtual void DisplayPeople(List<Person> people)
        {
            DisplayHeader();
            foreach (var person in people)
            {
                Console.WriteLine(BuildDisplayLineForPerson(person));
            }

        }

        public virtual string BuildDisplayLineForPerson(Person person)
        {
            var builder = new StringBuilder();
            builder.Append(TrimLength(person.LastName).PadRight(16));
            builder.Append(TrimLength(person.FirstName).PadRight(16));
            builder.Append(person.GetGenderString().PadRight(16));
            builder.Append(TrimLength(person.FavoriteColor).PadRight(16));
            builder.Append(person.DateOfBirth.ToString("M/d/yyyy"));
            builder.Append(Environment.NewLine);
            return builder.ToString();
        }

        public virtual string TrimLength(string toTrim)
        {
            return toTrim.Length > 15 ? toTrim.Substring(0, 15) : toTrim;
        }

        public virtual void DisplayHeader()
        {
            Console.WriteLine("Last Name\tFirst Name\tGender\t\tFavoriteColor\tDOB");
            Console.WriteLine("=============== =============== =============== =============== ===============");
        }

        public virtual void DisplayArgException(ArgumentValidationException ex)
        {
            Console.WriteLine($"Exception occured: {ex.Message}." + Environment.NewLine);
            foreach (var validReason in ex.ValidationResults)
            {
                Console.WriteLine(validReason);
            }
        }

        public virtual List<Person> ReadFiles(string[] args)
        {
            var parsed = new List<Person>();
            var i = 0;
            foreach (var type in Enum.GetValues(typeof(ParserEnum)).Cast<ParserEnum>())
            {
                var read = _file.ReadAllLines(args[i]);
                var parser = _parserFactory.GetParserFor(type);
                parsed.AddRange(parser.Parse(read.ToList()));
                i++;
            }
            return parsed;
        }

        public virtual void DisplayHelp()
        {
            Console.WriteLine("Application Help");
            Console.WriteLine("usage: RecordParse.exe [--help] [-h] pipe-delimit-file comma-delimit-file space-delimit-file");
        }
    }
}
