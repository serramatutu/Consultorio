using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using ConsultorioAPI.Providers;

[assembly: OwinStartup(typeof(ConsultorioAPI.App_Start.Startup))]

namespace ConsultorioAPI.App_Start
{
    public partial class Startup
    {
        const int TOKEN_EXPIRATION = 30; // horas
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static string PublicClientId { get; private set; }
        public static string ExternalAuthPageUrl { get; private set; }

        /// <summary>
        /// Configura os serviços de autorização do aplicativo
        /// </summary>
        public void ConfigureAuth(IAppBuilder app)
        {
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(TOKEN_EXPIRATION),
                // TODO: HTTPS
                AllowInsecureHttp = true
            };

            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
