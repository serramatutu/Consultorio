using System.Web.Http;

namespace ConsultorioAPI.App_Start
{
    public class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ConsultorioApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}