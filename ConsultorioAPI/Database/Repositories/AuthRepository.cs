using ConsultorioAPI.Database;
using ConsultorioAPI.Models;
using ConsultorioAPI.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace ConsultorioAPI.Data
{
    public class AuthRepository
    {
        public async Task<IdentityResult> RegisterUser(CadastroUserModel data, string role)
        {
            LoginUsuario user = new LoginUsuario
            {
                Id = Guid.NewGuid(),
                UserName = data.UserName,
                Email = data.Email
            };

            using (var userManager = new UserManager<LoginUsuario, Guid>(new ConsultorioUserStore(new ConsultorioDbContext())))
            {
                var result = await userManager.CreateAsync(user, data.Senha);

                if (!string.IsNullOrEmpty(role) && result.Succeeded)
                    userManager.AddToRole(user.Id, role);

                return result;
            }
        }

        public async Task<LoginUsuario> FindUser(string userName, string senha)
        {
            using (var userManager = new UserManager<LoginUsuario, Guid>(new ConsultorioUserStore(new ConsultorioDbContext())))
            {
                LoginUsuario user = await userManager.FindAsync(userName, senha);

                return user;
            }
        }
    }
}