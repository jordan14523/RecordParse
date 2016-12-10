﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using RecordParse.CLI.Interfaces;
using RecordParse.Shared;
using RecordParse.Shared.Interfaces;
using RecordParse.Shared.Model;
using RecordParse.Shared.Parsers;

namespace RecordParse.CLI
{
    public class DIConfig
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<PersonSerializer>().As<ISerializer<Person>>();
            builder.RegisterType<ParserFactory<Person>>().As<IParserFactory<Person>>();
            builder.RegisterType<PersonSorter>().As<IPersonSorter>();
            builder.RegisterType<FileAdapter>().As<IFile>();
            builder.RegisterType<ArgumentValidator>().As<IArgumentValidator>();
            builder.RegisterType<UIManager>().As<IUIManager>();

            return builder.Build();
        }
    }
}