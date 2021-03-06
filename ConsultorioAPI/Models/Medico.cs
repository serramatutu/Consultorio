﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models
{
    [Table("Medico")]
    public class Medico
    {
        public Medico()
        { }

        [Key, Column(Order = 0)]
        public virtual Guid Id { get; set; }

        [Column(TypeName = "varchar", Order = 1), MaxLength(7)]
        [Key]
        [Required]
        public virtual string CRM { get; set; }

        [Column("IdUsuario")]
        [JsonIgnore]
        public virtual LoginUsuario DadosLogin { get; set; }

        [Required]
        [Column(TypeName = "varchar"), MaxLength(40)]
        public virtual string Nome { get; set; }

        [Required]
        public virtual Especialidade Especialidade { get; set; }

        [Column(TypeName = "varchar"), MaxLength(14)]
        [Required]
        public virtual string Celular { get; set; }

        [Column(TypeName = "varchar"), MaxLength(14)]
        [Required]
        public virtual string Telefone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual DateTime DataNasc { get; set; }

        [JsonIgnore]
        public virtual ICollection<Consulta> Consultas { get; set; }

        //public virtual Bitmap Foto { get; set; }
    }
}