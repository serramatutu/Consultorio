using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models
{
    public class Paciente
    {
        public virtual Guid Id { get; set; }

        [Column("IdUsuario")]
        public virtual LoginUsuario DadosLogin { get; set; }

        [Column(TypeName = "varchar(14)")]
        public virtual string Telefone { get; set; }

        public virtual DateTime DataNasc { get; set; }

        //public virtual Bitmap Foto { get; set; }

        [Column(TypeName = "varchar(50)")]
        public virtual string Endereco { get; set; }
    }
}