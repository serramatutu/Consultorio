using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models
{
    public class Paciente
    {
        [Column("IdUsuario")]
        public virtual ConsultorioUser DadosLogin { get; set; }

        [Column(TypeName = "varchar(14)")]
        public virtual string Telefone { get; set; }
    }
}