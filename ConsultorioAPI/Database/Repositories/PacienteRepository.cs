using ConsultorioAPI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultorioAPI.Database.Repositories
{
    public class PacienteRepository : ConsultorioBaseRepository
    {
        UserManager<LoginUsuario, Guid> _userManager;

        public PacienteRepository(ConsultorioDbContext ctx) : base(ctx)
        {
            _userManager = new UserManager<LoginUsuario, Guid>(new ConsultorioUserStore(ctx));
        }

        public async Task<ResultadoOperacao> CreateAsync(Paciente p, string userName, string senha)
        {
            p.DadosLogin = await _userManager.FindAsync(userName, senha);
            _ctx.Pacientes.Add(p);

            int ar = await _ctx.SaveChangesAsync();

            if (ar < 1)
                return ResultadoOperacao.ErroBD;

            return ResultadoOperacao.Ok;
        }

        public Paciente GetPacienteFromUsername(string username)
        {
            LoginUsuario loginUsuario = _ctx.Usuarios.FirstOrDefault(x => x.UserName == username);
            if (loginUsuario == null)
                return null;

            return _ctx.Pacientes.FirstOrDefault(p => p.DadosLogin.Id.Equals(loginUsuario.Id));
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    _userManager.Dispose();
                    base.Dispose(disposing);
        }
    }
}