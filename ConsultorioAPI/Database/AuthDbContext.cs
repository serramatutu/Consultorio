

using Microsoft.AspNet.Identity.EntityFramework;

namespace ConsultorioAPI.Database.Contexts
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext()
            : base("ConexaoBD") // Utiliza a connection string 'ConexaoBD'
        {
        }
    }
}