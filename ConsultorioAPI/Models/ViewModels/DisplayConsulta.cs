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
            ComentarioMedico = c.ComentarioMedico;
            ComentarioPaciente = c.ComentarioPaciente;
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
        [Display(Name = "comentario do médico")]
        public string ComentarioMedico { get; set; }

        [Required]
        [Display(Name = "comentario do paciente")]
        public string ComentarioPaciente { get; set; }

        [Required]
        [Display(Name = "status")]
        public StatusConsulta Status { get; set; }
    }
}