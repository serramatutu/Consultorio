using ConsultorioAPI.Database;
using ConsultorioAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace ConsultorioAPI.Data
{
    public class AuthRepository : IDisposable
    {
        private ConsultorioDbContext _ctx;

        private UserManager<LoginUsuario, Guid> _userManager;

        public AuthRepository()
        {
            _ctx = new ConsultorioDbContext("ConexaoBD"); // Utiliza a connection string do BD para criar contexto de conexão
            _userManager = new UserManager<LoginUsuario, Guid>(new ConsultorioUserStore(_ctx));
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

            if (!string.IsNullOrEmpty(role))
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
            _ctx.Dispose();
            _userManager.Dispose();
        }
    }
}