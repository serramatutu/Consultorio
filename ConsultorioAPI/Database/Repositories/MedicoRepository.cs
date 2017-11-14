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
    }
}