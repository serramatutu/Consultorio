using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using System.Web.Http;

[assembly: OwinStartup(typeof(ConsultorioAPI.App_Start.Startup))]

namespace ConsultorioAPI.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var config = new HttpConfiguration();
            WebApiConfig.Configure(config);
            app.UseWebApi(config);

            app.UseCors(CorsOptions.AllowAll);
        }
    }
}
