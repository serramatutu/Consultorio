using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models
{
    public enum StatusConsulta
    {
        Agendada,
        Cancelada,
        Realizada
    }

    public class Consulta
    {
        public virtual Guid Id { get; set; }

        protected DateTime _dataHora;
        public virtual DateTime DataHora
        {
            get
            {
                return _dataHora;
            }
            set
            {
                if (value < DateTime.Now)
                    throw new ArgumentException("Não pode marcar consulta no passado");

                if (value.Hour < 9 || value.Hour > 17 || (value.Hour > 12 && value.Hour < 14))
                    throw new ArgumentException("Horario invalido");

                _dataHora = value;
            }
        }

        protected int _duracao;
        /// <summary>
        /// Duração em minutos
        /// </summary>
        public virtual int Duracao
        {
            get
            {
                return _duracao;
            }
            set
            {
                if (value < 30 || value > 60)
                    throw new ArgumentException("Duracao invalida");

                if (_dataHora.Hour + Duracao / 60 > 17)
                    throw new ArgumentException("Duracao invalida");

                _duracao = value;
            }
        }

        /// <summary>
        /// Motivo pela qual foi cancelada, prescrições etc
        /// </summary>
        [Column(TypeName = "nvarchar")]
        public virtual string Comentario { get; set; }

        public virtual Medico MedicoResponsavel { get; set; }

        public virtual Paciente Paciente { get; set; }
    }
}