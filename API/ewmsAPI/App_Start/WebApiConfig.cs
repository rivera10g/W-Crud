using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using ewms.common.models.entity;

using ewms.common.models;
using System.Web.Http.Cors;

namespace ewmsAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //bcgHelper.saveLog("Application Start", "WebApi.Register");

            //HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
            //HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.InitializeForProduction(9091, "mr4792729");

            var cors = new EnableCorsAttribute("*", "*", "*");

            config.EnableCors(cors);

            // oData
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<VW_SBO_CUSTOMERS>("Customers");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Controllers with Actions
            // To handle routes like `/api/login/validuser`
            config.Routes.MapHttpRoute(
                name: "ControllerAndAction",
                routeTemplate: "api/{controller}/{action}"
            );

            //Common.param.loadParameters();
        }
    }
}
