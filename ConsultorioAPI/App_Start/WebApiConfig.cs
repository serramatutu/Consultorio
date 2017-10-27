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
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Respostas em formato JSON
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

        public static void Register(HttpConfiguration config)
        {
            // Permite que requests Cross-Origin sejam feitas
            var cors = new EnableCorsAttribute(
                origins: "*",
                headers: "*",
                methods: "*",
                exposedHeaders: "*");
            config.EnableCors(cors);

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
        }
    }
}