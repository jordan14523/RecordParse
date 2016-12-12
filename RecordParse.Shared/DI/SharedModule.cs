using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using RecordParse.Shared.Interfaces;
using RecordParse.Shared.Model;
using RecordParse.Shared.Parsers;

namespace RecordParse.Shared.DI
{
    [ExcludeFromCodeCoverage]
    public class SharedModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PersonSerializer>().As<ISerializer<Person>>();
            builder.RegisterType<ParserFactory<Person>>().As<IParserFactory<Person>>();
            builder.RegisterType<PersonSorter>().As<IPersonSorter>();
        }
    }
}
