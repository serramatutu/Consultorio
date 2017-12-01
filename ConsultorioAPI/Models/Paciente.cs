using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models
{
    public class Paciente
    {
        public Paciente()
        { }

        [Key]
        public virtual Guid Id { get; set; }

        [Column("IdUsuario")]
        [JsonIgnore]
        public virtual LoginUsuario DadosLogin { get; set; }

        [Required]
        [Column(TypeName = "varchar"), MaxLength(40)]
        public virtual string Nome { get; set; }

        [Column(TypeName = "varchar"), MaxLength(14)]
        [Required]
        public virtual string Telefone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual DateTime DataNasc { get; set; }

        //public virtual Bitmap Foto { get; set; }

        [Column(TypeName = "nvarchar"), MaxLength(50)]
        [Required]
        public virtual string Endereco { get; set; }

        [JsonIgnore]
        public virtual ICollection<Consulta> Consultas { get; set; }
    }
}