using ConsultorioAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Database.Repositories
{
    public class MedicoRepository : ConsultorioBaseRepository
    {
        public MedicoRepository(ConsultorioDbContext ctx) : base(ctx)
        { }

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
    }
}