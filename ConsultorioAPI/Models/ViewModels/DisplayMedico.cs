using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models.ViewModels
{
    public class DisplayMedico
    {
        public DisplayMedico(Medico m)
        {
            Celular = m.Celular;
            Telefone = m.Telefone;
            Especialidade = m.Especialidade.Nome;
            Nome = m.Nome;
            CRM = m.CRM;
            DataNasc = m.DataNasc;
        }

        public virtual string Nome { get; set; }

        public virtual string Especialidade { get; set; }

        public virtual string Celular { get; set; }

        public virtual string Telefone { get; set; }

        public virtual string CRM { get; set; }

        public virtual DateTime DataNasc { get; set; }
    }
}