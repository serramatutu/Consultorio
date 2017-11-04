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
        : IUserStore<ConsultorioUser, int>,
          IUserClaimStore<ConsultorioUser, int>,
          IUserPasswordStore<ConsultorioUser, int>,
          IUserSecurityStampStore<ConsultorioUser, int>
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

        public Task CreateAsync(ConsultorioUser user)
        {
            _ctx.Usuarios.Add(user);
            //_ctx.Configuration.ValidateOnSaveEnabled = false;
            return _ctx.SaveChangesAsync();
        }

        public Task DeleteAsync(ConsultorioUser user)
        {
            _ctx.Usuarios.Remove(user);
            _ctx.Configuration.ValidateOnSaveEnabled = false;
            return _ctx.SaveChangesAsync();
        }

        public void Dispose()
        {
            userStore.Dispose();
        }

        public Task<ConsultorioUser> FindByIdAsync(int userId)
        {
            IQueryable<ConsultorioUser> users = _ctx.Usuarios.Where(u => u.Id == userId);

            if (users == null)
                return null;
            return users.FirstOrDefaultAsync(); // Retorna o primeiro usuário encontrado (ou null se não encontrar)
        }

        public Task<ConsultorioUser> FindByNameAsync(string userName)
        {
            IQueryable<ConsultorioUser> users = _ctx.Usuarios.Where(u => u.UserName.ToLower() == userName.ToLower());

            if (users == null)
                return null;
            return users.FirstOrDefaultAsync(); // Retorna o primeiro usuário encontrado (ou null se não encontrar)
        }

        public Task<string> GetPasswordHashAsync(ConsultorioUser user)
        {
            var identityUser = ToIdentityUser(user);
            var task = userStore.GetPasswordHashAsync(identityUser);
            SetConsultorioUser(user, identityUser);
            return task;
        }

        public Task<string> GetSecurityStampAsync(ConsultorioUser user)
        {
            var identityUser = ToIdentityUser(user);
            var task = userStore.GetSecurityStampAsync(identityUser);
            SetConsultorioUser(user, identityUser);
            return task;
        }

        public Task<bool> HasPasswordAsync(ConsultorioUser user)
        {
            var identityUser = ToIdentityUser(user);
            var task = userStore.HasPasswordAsync(identityUser);
            SetConsultorioUser(user, identityUser);
            return task;
        }

        public Task SetPasswordHashAsync(ConsultorioUser user, string passwordHash)
        {
            var identityUser = ToIdentityUser(user);
            // Utiliza o do prório UserStore
            var task = userStore.SetPasswordHashAsync(identityUser, passwordHash);
            SetConsultorioUser(user, identityUser);
            return task;
        }

        public Task SetSecurityStampAsync(ConsultorioUser user, string stamp)
        {
            var identityUser = ToIdentityUser(user);
            // Utiliza o do próprio userstore
            var task = userStore.SetSecurityStampAsync(identityUser, stamp);
            SetConsultorioUser(user, identityUser);
            return task;
        }

        public Task UpdateAsync(ConsultorioUser user)
        {
            _ctx.Usuarios.Attach(user);
            _ctx.Entry(user).State = EntityState.Modified;
            _ctx.Configuration.ValidateOnSaveEnabled = false;
            return _ctx.SaveChangesAsync();
        }

        public Task<IList<Claim>> GetClaimsAsync(ConsultorioUser user)
        {
            IList<Claim> result = user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
            return Task.FromResult(result);
        }

        public Task AddClaimAsync(ConsultorioUser user, Claim claim)
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

        public Task RemoveClaimAsync(ConsultorioUser user, Claim claim)
        {
            user.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            return Task.FromResult(0);
        }

        private static void SetConsultorioUser(ConsultorioUser user, IdentityUser identityUser)
        {
            user.HashSenha = identityUser.PasswordHash;
            user.SecurityStamp = identityUser.SecurityStamp;
            user.Id = Convert.ToInt32(identityUser.Id);
            user.UserName = identityUser.UserName;
        }

        private IdentityUser ToIdentityUser(ConsultorioUser user)
        {
            return new IdentityUser
            {
                Id = user.Id.ToString(),
                PasswordHash = user.HashSenha,
                SecurityStamp = user.SecurityStamp,
                UserName = user.UserName
            };
        }
    }
}