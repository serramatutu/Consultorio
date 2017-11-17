using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models.ViewModels
{
    public class DisplayConsulta
    {
        public DisplayConsulta(Consulta c)
        {
            DataHora = c.DataHora;
            Duracao = c.Duracao;
            CRMMedicoResponsavel = c.MedicoResponsavel.CRM;
            NomeMedico = c.MedicoResponsavel.Nome;
            Comentario = c.Comentario;
            Status = c.Status;
        }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "horário")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataHora { get; set; }

        [Required]
        [Display(Name = "duração")]
        public int Duracao { get; set; }

        [Required]
        [Display(Name = "CRM")]
        public string CRMMedicoResponsavel { get; set; }

        [Required]
        [Display(Name = "nome médico responsável")]
        public string NomeMedico { get; set; }

        [Required]
        [Display(Name = "Comentario")]
        public string Comentario { get; set; }

        [Required]
        [Display(Name = "Comentario")]
        public StatusConsulta Status { get; set; }
    }
}