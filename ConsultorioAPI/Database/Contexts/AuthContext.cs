

using Microsoft.AspNet.Identity.EntityFramework;

namespace ConsultorioAPI.Database.Contexts
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {

        }
    }
}