using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models.ViewModels
{
    public class AgendamentoConsulta
    {
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "horário")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataHora { get; set; }

        [Required]
        [Display(Name = "duração")]
        public int Duracao { get; set; }

        [Required]
        [Display(Name = "CRM")]
        public string CRMMedicoResponsavel { get; set; }
    }
}