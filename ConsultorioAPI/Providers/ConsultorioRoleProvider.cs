using ConsultorioAPI.Database;
using ConsultorioAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ConsultorioAPI.Providers
{
    public class ConsultorioRoleProvider : RoleProvider
    {

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            using (ConsultorioDbContext ctx = new ConsultorioDbContext())
            {
                foreach (var user in ctx.Users)
                    user.Roles.AddRange(roleNames.Select(x => new UserRole(x)));
            }
        }

        public override string ApplicationName { get; set; }

        public override void CreateRole(string roleName)
        {
            if (RoleExists(roleName))
                throw new ProviderException("O role especificado já existe");

            using (ConsultorioDbContext ctx = new ConsultorioDbContext())
            {
                ctx.Roles.Add(new UserRole(roleName));
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            if (!RoleExists(roleName))
                    throw new ProviderException("O role especificado não existe.");
            if (throwOnPopulatedRole && GetUsersInRole(roleName).Length > 0)
                    throw new ProviderException("Não pôde deletar role populado.");

            using (ConsultorioDbContext ctx = new ConsultorioDbContext()) {
                ctx.Roles.Remove(ctx.Roles.First(x => x.Name == roleName)); // Remove o role efetivamente
                return true;
            }

            return false;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            using (ConsultorioDbContext ctx = new ConsultorioDbContext())
            {
                return ctx.Users.Where(
                    x => x.UserName.Contains(usernameToMatch) && x.Roles.Exists(y => y.Name == roleName) // Encontra o usuário pelos critérios
                ).Select(x => x.UserName).ToArray(); // Obtém apenas um vetor de nomes de usuários
            }
        }

        public override string[] GetAllRoles()
        {
            using (ConsultorioDbContext ctx = new ConsultorioDbContext())
            {
                return ctx.Roles.Select(x => x.Name).ToArray();
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            using (ConsultorioDbContext ctx = new ConsultorioDbContext())
            {
                var user = ctx.Users.FirstOrDefault(x => x.UserName == username);
                if (user == null)
                {
                    return null;
                }
                else
                {
                    string[] ret = user.Roles.Select(x => x.Name).ToArray();
                    return ret;
                }
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            using (ConsultorioDbContext ctx = new ConsultorioDbContext())
            {
                return ctx.Users.Where(x => x.Roles.Exists(y => y.Name == roleName)) // Seleciona os usuários caso contenham o role
                    .Select(x => x.UserName).ToArray(); // Seleciona os nomes destes usuários e transforma em vetor
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (ConsultorioDbContext ctx = new ConsultorioDbContext())
            {
                return ctx.Users.First(x => x.UserName == username).Roles.Exists(x => x.Name == roleName);
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            using (ConsultorioDbContext ctx = new ConsultorioDbContext())
            {
                var users = ctx.Users.Where(x => usernames.Contains(x.UserName)); // Seleciona os usuários designados

                foreach (var user in users)
                    user.Roles.RemoveAll(x => roleNames.Contains(x.Name)); // Remove os roles
            }
        }

        public override bool RoleExists(string roleName)
        {
            using (ConsultorioDbContext ctx = new ConsultorioDbContext())
            {
                return ctx.Roles.Any(x => x.Name == roleName);
            }
        }
    }
}