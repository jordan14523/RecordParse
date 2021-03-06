﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Http;
using Autofac.Integration.WebApi;
using RecordParse.API.App_Start;

namespace RecordParse.API
{
    public static class WebApiConfig
    {

        [ExcludeFromCodeCoverage]
        public static void Register(HttpConfiguration config)
        {

            //DI Setup
            config.DependencyResolver = new AutofacWebApiDependencyResolver(DIConfig.GetContainer());

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
