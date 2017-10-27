using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using ConsultorioAPI.Database.Contexts;

namespace ConsultorioAPI.Database
{
    public class ConsultorioUserStore
        : IUserStore<ConsultorioUser>,
          IUserPasswordStore<ConsultorioUser>,
          IUserSecurityStampStore<ConsultorioUser>
    {
        UserStore<IdentityUser> userStore;

        public ConsultorioUserStore(DbContext ctx)
        {
            userStore = new UserStore<IdentityUser>(ctx);
        }

        public ConsultorioUserStore() : this (new ConsultorioDbContext())
        { }

        public Task CreateAsync(ConsultorioUser user)
        {
            var context = userStore.Context as ConsultorioDbContext;
            context.Users.Add(user);
            context.Configuration.ValidateOnSaveEnabled = false;
            return context.SaveChangesAsync();
        }

        public Task DeleteAsync(ConsultorioUser user)
        {
            var context = userStore.Context as ConsultorioDbContext;
            context.Users.Remove(user);
            context.Configuration.ValidateOnSaveEnabled = false;
            return context.SaveChangesAsync();
        }

        public void Dispose()
        {
            userStore.Dispose();
        }

        public Task<ConsultorioUser> FindByIdAsync(string userId)
        {
            var context = userStore.Context as ConsultorioDbContext;
            IQueryable<ConsultorioUser> users = context.Users.Where(u => u.Id.ToLower() == userId.ToLower());

            if (users == null)
                return null;
            return users.FirstAsync(); // Retorna o primeiro usuário encontrado
        }

        public Task<ConsultorioUser> FindByNameAsync(string userName)
        {
            var context = userStore.Context as ConsultorioDbContext;
            IQueryable<ConsultorioUser> users = context.Users.Where(u => u.UserName.ToLower() == userName.ToLower());

            if (users == null)
                return null;
            return users.FirstAsync(); // Retorna o primeiro usuário encontrado
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
            var context = userStore.Context as ConsultorioDbContext;
            context.Users.Attach(user);
            context.Entry(user).State = EntityState.Modified;
            context.Configuration.ValidateOnSaveEnabled = false;
            return context.SaveChangesAsync();
        }

        private static void SetConsultorioUser(ConsultorioUser user, IdentityUser identityUser)
        {
            user.PasswordHash = identityUser.PasswordHash;
            user.SecurityStamp = identityUser.SecurityStamp;
            user.Id = identityUser.Id;
            user.UserName = identityUser.UserName;
        }

        private IdentityUser ToIdentityUser(ConsultorioUser user)
        {
            return new IdentityUser
            {
                Id = user.Id,
                PasswordHash = user.PasswordHash,
                SecurityStamp = user.SecurityStamp,
                UserName = user.UserName
            };
        }

    }
}