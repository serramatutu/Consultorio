using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(ConsultorioAPI.App_Start.Startup))]

namespace ConsultorioAPI.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //app.UseCors(CorsOptions.AllowAll);
        }
    }
}
