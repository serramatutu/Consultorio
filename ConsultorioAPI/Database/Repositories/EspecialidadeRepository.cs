using ConsultorioAPI.Models;
using ConsultorioAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ConsultorioAPI.Database.Repositories
{
    public class EspecialidadeRepository : ConsultorioBaseRepository
    {
        public EspecialidadeRepository(ConsultorioDbContext ctx) : base(ctx)
        { }

        public async Task<ResultadoOperacao> CreateAsync(string nomeEspecialidade)
        {
            if (_ctx.Especialidades.Any(x => x.Nome.ToLower() == nomeEspecialidade.ToLower()))
                return new ResultadoOperacao()
                {
                    Sucesso = false,
                    Mensagem = "Especialidade já existente"
                };

            _ctx.Especialidades.Add(new Especialidade(nomeEspecialidade));
            if (await _ctx.SaveChangesAsync() < 1)
                return ResultadoOperacao.ErroBD;

            return ResultadoOperacao.Ok;
        }

        public Especialidade FindById(int id)
        {
            return _ctx.Especialidades.Where(x => x.Id == id).FirstOrDefault();
        }

        public Especialidade[] GetAll()
        {
            return _ctx.Especialidades.ToArray();
        }

        public EstatisticaEspecialidade[] GetEstatisticas()
        {
            return _ctx.Consultas.Where(c => DbFunctions.AddDays(c.DataHora, 1) > DateTime.Today && 
                                        c.DataHora < DateTime.Today && 
                                        c.Status == StatusConsulta.Realizada)
                                 .Select(c => c.Medico.Especialidade)
                                 .GroupBy(e => e.Id)
                                 .Select(group => new EstatisticaEspecialidade()
                                 {
                                     Especialidade = group.FirstOrDefault(),
                                     ConsultasNoMes = group.Count()
                                 }).ToArray();
        }
    }
}