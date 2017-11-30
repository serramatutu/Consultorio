using ConsultorioAPI.Database;
using ConsultorioAPI.Models;
using ConsultorioAPI.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsultorioAPI.Data
{
    public class AuthRepository : IDisposable
    {
        private UserManager<LoginUsuario, Guid> _userManager;

        public AuthRepository(ConsultorioDbContext ctx)
        {
            _userManager = new UserManager<LoginUsuario, Guid>(new ConsultorioUserStore(ctx));
        }

        public async Task<IList<string>> GetPapeisAsync(Guid idUsuario)
        {
            return await _userManager.GetRolesAsync(idUsuario);
        }

        public async Task<IdentityResult> RegisterUser(string userName, string email, string senha, string papel)
        {
            LoginUsuario user = new LoginUsuario
            {
                Id = Guid.NewGuid(),
                UserName = userName,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, senha);

            if (!string.IsNullOrEmpty(papel) && result.Succeeded)
                _userManager.AddToRole(user.Id, papel);

            return result;
        }

        public async Task<LoginUsuario> FindUser(string userName, string senha)
        {
            LoginUsuario user = await _userManager.FindAsync(userName, senha);

            return user;
        }

        public async Task<LoginUsuario> FindUser(string userName)
        {
            LoginUsuario user = await _userManager.FindByNameAsync(userName);

            return user;
        }

        protected bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _userManager.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}