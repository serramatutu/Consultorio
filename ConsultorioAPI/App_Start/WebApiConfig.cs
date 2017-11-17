using Microsoft.Owin.Security.OAuth;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ConsultorioAPI.App_Start
{
    public class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            //config.MapHttpAttributeRoutes();
            //config.Routes.MapHttpRoute(
            //    name: "Default",
            //    routeTemplate: "{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //// Respostas em formato JSON
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
        }

        public static void Register(HttpConfiguration config)
        {
            // CORS
            //var cors = new EnableCorsAttribute(Globals.CLIENT_URL, "*", "*");
            //config.EnableCors(cors);

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Rotas
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Respostas em formato JSON (e não XML)
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}