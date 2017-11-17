using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(ConsultorioAPI.App_Start.Startup))]

namespace ConsultorioAPI.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            ConfigureAuth(app);
        }
    }
}
