using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace WebApiTest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Select",
                routeTemplate: "api/{controller}/select/{date1}/{date2}",
                defaults: new { date1 = RouteParameter.Optional, date2 = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "SelLog",
                routeTemplate: "api/{controller}/log",
                defaults: new { }
            );

            config
                .Formatters
                .JsonFormatter
                .SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
