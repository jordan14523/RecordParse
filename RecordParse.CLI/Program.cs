using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
    class Program
    {
        private static readonly IContainer _diContainer =  DIConfig.GetContainer();

        static void Main(string[] args)
        {
            var uiManager = _diContainer.Resolve<IUIManager>();
            uiManager.Start(args);
        }
    }
}
