using ConsultorioAPI.Data;
using ConsultorioAPI.Database;
using ConsultorioAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace ConsultorioAPI.Providers
{
    public class ConsultorioOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (AuthRepository repo = new AuthRepository(new ConsultorioDbContext()))
            {
                LoginUsuario user = await repo.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "Usuário ou senha está incorreto");
                    return;
                }

                var papeis = await repo.GetPapeisAsync(user.Id) as List<string>;

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                foreach(string papel in papeis)
                {
                    identity.AddClaim(new Claim("sub", context.UserName));
                    identity.AddClaim(new Claim("role", papel));
                }

                context.Validated(identity);
            }
        }
    }
}