using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using ConsultorioAPI.Models;
using System.Security.Claims;
using System.Linq;

namespace ConsultorioAPI.Database
{
    public class ConsultorioUserStore
        : IUserStore<LoginUsuario, Guid>,
          IUserRoleStore<LoginUsuario, Guid>,
          IUserClaimStore<LoginUsuario, Guid>,
          IUserPasswordStore<LoginUsuario, Guid>,
          IUserSecurityStampStore<LoginUsuario, Guid>
    {
        UserStore<IdentityUser> userStore;
        ConsultorioDbContext _ctx;

        public ConsultorioUserStore(ConsultorioDbContext ctx)
        {
            _ctx = ctx;
            userStore = new UserStore<IdentityUser>(ctx);
        }

        public ConsultorioUserStore() : this (new ConsultorioDbContext())
        { }

        public Task CreateAsync(LoginUsuario user)
        {
            _ctx.Usuarios.Add(user);
            //_ctx.Configuration.ValidateOnSaveEnabled = false;
            return _ctx.SaveChangesAsync();
        }

        public Task DeleteAsync(LoginUsuario user)
        {
            _ctx.Usuarios.Remove(user);
            _ctx.Configuration.ValidateOnSaveEnabled = false;
            return _ctx.SaveChangesAsync();
        }

        public void Dispose()
        {
            userStore.Dispose();
        }

        public Task<LoginUsuario> FindByIdAsync(Guid userId)
        {
            IQueryable<LoginUsuario> users = _ctx.Usuarios.Where(u => u.Id == userId);

            if (users == null)
                return null;
            return users.FirstOrDefaultAsync(); // Retorna o primeiro usuário encontrado (ou null se não encontrar)
        }

        public Task<LoginUsuario> FindByNameAsync(string userName)
        {
            IQueryable<LoginUsuario> users = _ctx.Usuarios.Where(u => u.UserName.ToLower() == userName.ToLower());

            if (users == null)
                return null;
            return users.FirstOrDefaultAsync(); // Retorna o primeiro usuário encontrado (ou null se não encontrar)
        }

        public Task<string> GetPasswordHashAsync(LoginUsuario user)
        {
            var identityUser = ToIdentityUser(user);
            var task = userStore.GetPasswordHashAsync(identityUser);
            SetConsultorioUser(user, identityUser);
            return task;
        }

        public Task<string> GetSecurityStampAsync(LoginUsuario user)
        {
            var identityUser = ToIdentityUser(user);
            var task = userStore.GetSecurityStampAsync(identityUser);
            SetConsultorioUser(user, identityUser);
            return task;
        }

        public Task<bool> HasPasswordAsync(LoginUsuario user)
        {
            var identityUser = ToIdentityUser(user);
            var task = userStore.HasPasswordAsync(identityUser);
            SetConsultorioUser(user, identityUser);
            return task;
        }

        public Task SetPasswordHashAsync(LoginUsuario user, string passwordHash)
        {
            var identityUser = ToIdentityUser(user);
            // Utiliza o do prório UserStore
            var task = userStore.SetPasswordHashAsync(identityUser, passwordHash);
            SetConsultorioUser(user, identityUser);
            return task;
        }

        public Task SetSecurityStampAsync(LoginUsuario user, string stamp)
        {
            var identityUser = ToIdentityUser(user);
            // Utiliza o do próprio userstore
            var task = userStore.SetSecurityStampAsync(identityUser, stamp);
            SetConsultorioUser(user, identityUser);
            return task;
        }

        public Task UpdateAsync(LoginUsuario user)
        {
            _ctx.Usuarios.Attach(user);
            _ctx.Entry(user).State = EntityState.Modified;
            _ctx.Configuration.ValidateOnSaveEnabled = false;
            return _ctx.SaveChangesAsync();
        }

        public Task<IList<Claim>> GetClaimsAsync(LoginUsuario user)
        {
            IList<Claim> result = user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
            return Task.FromResult(result);
        }

        public Task AddClaimAsync(LoginUsuario user, Claim claim)
        {
            if (!user.Claims.Any(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value)) // Checa se já não existe
            {
                user.Claims.Add(new IdentityUserClaim
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                });
            }

            return Task.FromResult(0);
        }

        public Task RemoveClaimAsync(LoginUsuario user, Claim claim)
        {
            user.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            return Task.FromResult(0);
        }

        private static void SetConsultorioUser(LoginUsuario user, IdentityUser identityUser)
        {
            user.HashSenha = identityUser.PasswordHash;
            user.SecurityStamp = identityUser.SecurityStamp;
            user.Id = Guid.Parse(identityUser.Id);
            user.UserName = identityUser.UserName;
        }

        private IdentityUser ToIdentityUser(LoginUsuario user)
        {
            return new IdentityUser
            {
                Id = user.Id.ToString(),
                PasswordHash = user.HashSenha,
                SecurityStamp = user.SecurityStamp,
                UserName = user.UserName
            };
        }

        public Task AddToRoleAsync(LoginUsuario user, string roleName)
        {
            _ctx.Set<PapelUsuario>().Where(x => x.Nome == roleName).Single().Usuarios.Add(user);
            return _ctx.SaveChangesAsync();
        }

        public Task RemoveFromRoleAsync(LoginUsuario user, string roleName)
        {
            _ctx.Set<LoginUsuario>().Where(x => x.Id.Equals(user.Id)).Single().Papeis.RemoveAll(x => x.Nome == roleName);
            return _ctx.SaveChangesAsync();
        }

        public Task<IList<string>> GetRolesAsync(LoginUsuario user)
        {
            return new Task<IList<string>>(() =>
            {
                return user.Papeis.Select(x => x.Nome).ToList();
            });
        }

        public Task<bool> IsInRoleAsync(LoginUsuario user, string roleName)
        {
            return new Task<bool>(() =>
            {
                return user.Papeis.Exists(x => x.Nome == roleName);
            });
        }
    }
}