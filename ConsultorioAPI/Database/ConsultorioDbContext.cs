using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Database
{
    public class ConsultorioDbContext : DbContext
    {
        public ConsultorioDbContext() : base("ConexaoBD")
        {

        }

        public virtual IDbSet<ConsultorioUser> Users { get; set; }
    }
}