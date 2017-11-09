using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models
{
    public class Medico
    {
        public Medico()
        { }
        [Key]
        public virtual Guid Id { get; set; }

        [Column("IdUsuario")]
        [ForeignKey("Id")]
        public virtual LoginUsuario DadosLogin { get; set; }

        public virtual Especialidade Especialidade { get; set; }

        [Column(TypeName = "varchar"), MaxLength(14)]
        public virtual string Celular { get; set; }

        [Column(TypeName = "varchar"), MaxLength(14)]
        public virtual string Telefone { get; set; }

        [Column(TypeName = "varchar"), MaxLength(7)]
        public virtual string CRM { get; set; }

        [DataType(DataType.Date)]
        public virtual DateTime DataNasc { get; set; }

        public virtual ICollection<Consulta> Consultas { get; set; }

        //public virtual Bitmap Foto { get; set; }
    }
}