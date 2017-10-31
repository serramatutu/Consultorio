using ConsultorioAPI.Database;
using ConsultorioAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace ConsultorioAPI.Data
{
    public class AuthRepository : IDisposable
    {
        private ConsultorioDbContext _ctx;

        private UserManager<ConsultorioUser> _userManager;

        public AuthRepository()
        {
            _ctx = new ConsultorioDbContext();
            _userManager = new UserManager<ConsultorioUser>(new ConsultorioUserStore(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(ConsultorioUser userModel)
        {
            ConsultorioUser user = new ConsultorioUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Senha);

            return result;
        }

        public async Task<ConsultorioUser> FindUser(string userName, string senha)
        {
            ConsultorioUser user = await _userManager.FindAsync(userName, senha);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }
    }
}