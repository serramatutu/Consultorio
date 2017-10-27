using ConsultorioAPI.Database;
using ConsultorioAPI.Database.Contexts;
using ConsultorioAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace ConsultorioAPI.Data
{
    public class AuthRepository : IDisposable
    {
        private AuthDbContext _ctx;

        private UserManager<ConsultorioUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthDbContext();
            _userManager = new UserManager<ConsultorioUser>(new ConsultorioUserStore(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            ConsultorioUser user = new ConsultorioUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<ConsultorioUser> FindUser(string userName, string password)
        {
            ConsultorioUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }
    }
}