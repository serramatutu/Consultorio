using System;
using System.Diagnostics;
using System.Web.Http;
using ConsultorioAPI.App_Start;
using System.Web;
using ConsultorioAPI.Database;

namespace ConsultorioAPI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //GlobalConfiguration.Configure(WebApiConfig.Configure);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.EnsureInitialized();
            System.Data.Entity.Database.SetInitializer<ConsultorioDbContext>(null); // Bugado mas vai
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
            {
                Trace.TraceError(ex.ToString());
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}