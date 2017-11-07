using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models
{
    public class Especialidade
    {
        public virtual Guid Id { get; set; }

        [Column(TypeName = "varchar(20)")]
        public virtual string Nome { get; set; }

        [Column(TypeName = "nvarchar")]
        public virtual string Descricao { get; set; }

        /// <summary>
        /// Médicos dessa especialidade
        /// </summary>
        public virtual List<Medico> Medicos { get; set; }
    }
}