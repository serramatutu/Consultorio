using ConsultorioAPI.Models;
using System;
using System.Collections.Generic;
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
            if (_ctx.Especialidades.Any(x => x.Nome == nomeEspecialidade))
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

        public Especialidade[] GetAll()
        {
            return _ctx.Especialidades.ToArray();
        }
    }
}