using ConsultorioAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ConsultorioAPI.Models.ViewModels;
using System.Data.Entity;
using ConsultorioAPI.Util;

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

        public EstatisticaMedico[] GetEstatisticas()
        {
            try
            {
                return _ctx.Medicos.Include(m => m.Consultas).Select(delegate(Medico m)
                    {
                        var consultasNoMes = m.Consultas.Where(c => c.DataHora.AddDays(30) > DateTime.Today).ToArray();
                        return new EstatisticaMedico()
                        {
                            Medico = m,
                            ConsultasNoMes = consultasNoMes.Length,
                            AvaliacaoMedia = (int?)consultasNoMes.Where(c => c.Avaliacao.Nota.HasValue)
                                                .Select(c => c.Avaliacao.Nota.Value)
                                                .AverageOrDefault()
                        };
                    }).ToArray();
                }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            return null;
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