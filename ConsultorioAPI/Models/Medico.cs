﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models
{
    public class Medico
    {
        public virtual Guid Id { get; set; }

        [Column("IdUsuario")]
        public virtual ConsultorioUser DadosLogin { get; set; }

        public virtual Especialidade Especialidade { get; set; }

        [Column(TypeName = "varchar(14)")]
        public virtual string Telefone { get; set; }
    }
}