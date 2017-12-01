using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models.ViewModels
{
    public class AgendamentoConsulta
    {
        protected DateTime _dataHora;

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "horário")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime DataHora
        {
            get
            {
                return _dataHora;
            }
            set
            {
                if (value < DateTime.UtcNow)
                    throw new ArgumentException("Não pode marcar consulta no passado");

                var dataFim = value.AddMinutes(Duracao);

                // Horários em UTC
                if (value.Hour < 11 || dataFim.Hour > 19 || (dataFim.Hour > 14 && value.Hour < 16))
                    throw new ArgumentException("Horário de consulta inválido");

                _dataHora = value;
            }
        }

        [Required]
        [Display(Name = "duração")]
        public int Duracao { get; set; }

        [Required]
        [Display(Name = "CRM")]
        public string CRMMedicoResponsavel { get; set; }
    }
}