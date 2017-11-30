using ConsultorioAPI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
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

        public async Task<ResultadoOperacao> CreateAsync(Paciente p, string userName)
        {
            p.Id = Guid.NewGuid();
            p.DadosLogin = await _userManager.FindByNameAsync(userName);
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

        #region Conta

        public ResultadoOperacao AlterarNome(string username, string nome)
        {
            Paciente p = GetPacienteFromUsername(username);
            if (p == null)
                return ResultadoOperacao.NotFound;

            p.Nome = nome;
            _ctx.Entry(p).State = EntityState.Modified;

            if (_ctx.SaveChanges() < 1)
                return ResultadoOperacao.ErroBD;

            return ResultadoOperacao.Ok;
        }

        public ResultadoOperacao AlterarEndereco(string username, string endereco)
        {
            Paciente p = GetPacienteFromUsername(username);
            if (p == null)
                return ResultadoOperacao.NotFound;

            p.Endereco = endereco;
            _ctx.Entry(p).State = EntityState.Modified;

            if (_ctx.SaveChanges() < 1)
                return ResultadoOperacao.ErroBD;

            return ResultadoOperacao.Ok;
        }

        public ResultadoOperacao AlterarTelefone(string username, string telefone)
        {
            Paciente p = GetPacienteFromUsername(username);
            if (p == null)
                return ResultadoOperacao.NotFound;

            p.Telefone = telefone;
            _ctx.Entry(p).State = EntityState.Modified;

            if (_ctx.SaveChanges() < 1)
                return ResultadoOperacao.ErroBD;

            return ResultadoOperacao.Ok;
        }

        public ResultadoOperacao AlterarDataNasc(string username, DateTime nasc)
        {
            Paciente p = GetPacienteFromUsername(username);
            if (p == null)
                return ResultadoOperacao.NotFound;

            p.DataNasc = nasc;
            _ctx.Entry(p).State = EntityState.Modified;

            if (_ctx.SaveChanges() < 1)
                return ResultadoOperacao.ErroBD;

            return ResultadoOperacao.Ok;
        }

        #endregion
    }
}