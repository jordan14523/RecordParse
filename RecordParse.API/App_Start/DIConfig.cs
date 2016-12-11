using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.WebApi;
using RecordParse.Shared.DI;

namespace RecordParse.API.App_Start
{
    public static class DIConfig
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<SharedModule>();
            builder.RegisterType<PersonService>().As<IPersonService>().SingleInstance();

            return builder.Build();
        }
    }
}