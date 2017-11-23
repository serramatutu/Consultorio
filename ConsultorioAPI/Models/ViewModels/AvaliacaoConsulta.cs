using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models.ViewModels
{
    public class AvaliacaoConsulta
    {
        public AvaliacaoConsulta()
        { }

        [Range(0, 10)]
        public int? Nota { get; set; } = null;

        [Column(TypeName = "ntext")]
        public string Comentario { get; set; } = null;
    }
}