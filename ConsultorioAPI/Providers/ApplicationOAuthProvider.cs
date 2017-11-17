using ConsultorioAPI.Data;
using ConsultorioAPI.Database;
using ConsultorioAPI.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                foreach (string papel in papeis)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, papel));
                }

                context.Validated(identity);


                AuthenticationTicket ticket = new AuthenticationTicket(identity, CreateProperties(papeis.ToArray()));
                context.Validated(ticket);
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public AuthenticationProperties CreateProperties(string[] papeis)
        {
            string papeisString = JsonConvert.SerializeObject(papeis);
            return new AuthenticationProperties(new Dictionary<string, string>
            {
                {"roles", papeisString}
            });
        }
    }
}