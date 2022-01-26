using System.Web.Http;

namespace KBZ.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }

            //);

            config.Routes.MapHttpRoute(
               name: "APIConfig",
               routeTemplate: "api/{controller}/{action}/{Type}",
               defaults: new { Type = RouteParameter.Optional }
           );

           



            //config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(
            //    config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType
            //        == "application/xml"));
            //config.Filters.Add(new JulyApiExceptionHandler());
        }
    }
}
