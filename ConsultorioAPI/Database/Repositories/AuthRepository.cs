using ConsultorioAPI.Database;
using ConsultorioAPI.Models;
using ConsultorioAPI.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace ConsultorioAPI.Data
{
    public class AuthRepository : IDisposable
    {
        private UserManager<LoginUsuario, Guid> _userManager;

        public AuthRepository()
        {
            _userManager = new UserManager<LoginUsuario, Guid>(new ConsultorioUserStore(new ConsultorioDbContext()));
        }

        public async Task<IdentityResult> RegisterUser(CadastroUserModel data, string role)
        {
            LoginUsuario user = new LoginUsuario
            {
                Id = Guid.NewGuid(),
                UserName = data.UserName,
                Email = data.Email,
            };

            var result = await _userManager.CreateAsync(user, data.Senha);

            if (!string.IsNullOrEmpty(role) && result.Succeeded)
                _userManager.AddToRole(user.Id, role);

            return result;
        }

        public async Task<LoginUsuario> FindUser(string userName, string senha)
        {
            LoginUsuario user = await _userManager.FindAsync(userName, senha);

            return user;
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }
    }
}