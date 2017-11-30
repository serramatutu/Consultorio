using ConsultorioAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ConsultorioAPI.Database.Repositories
{
    public class MedicoRepository : ConsultorioBaseRepository
    {
        UserManager<LoginUsuario, Guid> _userManager;

        public MedicoRepository(ConsultorioDbContext ctx) : base(ctx)
        {
            _userManager = new UserManager<LoginUsuario, Guid>(new ConsultorioUserStore(ctx));
        }

        public Medico[] GetMedicos(string[] especialidades)
        {
            if (especialidades.Length > 0)
                return _ctx.Medicos.Where(x => especialidades.Any(y => y == x.Especialidade.Nome)).ToArray();
            else
                return _ctx.Medicos.ToArray();
        }

        public Medico GetMedico(string username)
        {
            return _ctx.Medicos.FirstOrDefault(x => x.DadosLogin.UserName == username);
        }

        public async Task<ResultadoOperacao> CreateAsync(Medico medico, string userName, int idEspecialidade)
        {
            medico.Id = Guid.NewGuid();
            medico.DadosLogin = await _userManager.FindByNameAsync(userName);
            Especialidade especialidade = _ctx.Especialidades.Where(x => x.Id == idEspecialidade).FirstOrDefault();
            if (especialidade == null)
                return new ResultadoOperacao()
                {
                    Sucesso = false,
                    Mensagem = "Especialidade não encontrada"
                };
            medico.Especialidade = especialidade;
            _ctx.Medicos.Add(medico);

            int ar = await _ctx.SaveChangesAsync();

            if (ar < 1)
                return ResultadoOperacao.ErroBD;

            return ResultadoOperacao.Ok;
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