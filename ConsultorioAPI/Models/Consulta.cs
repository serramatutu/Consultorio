using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultorioAPI.Models
{
    public enum StatusConsulta
    {
        Agendada = 0,
        Cancelada = 1,
        Realizada = 2
    }

    [Table("Consulta")]
    public class Consulta
    {
        public Consulta()
        { }

        [Key]
        public virtual Guid Id { get; set; }

        protected DateTime _dataHora;

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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

                var dataFim = value.AddMinutes(Duracao);

                if (value.Hour < 9 || dataFim.Hour > 17 || (dataFim.Hour > 12 && value.Hour < 14))
                    throw new ArgumentException("Horário de consulta inválido");

                _dataHora = value;
            }
        }

        protected int _duracao = 30;
        /// <summary>
        /// Duração em minutos
        /// </summary>
        [Required]
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
        /// Prescrições etc
        /// </summary>
        [Column(TypeName = "ntext")]
        public virtual string ComentarioMedico { get; set; }

        /// <summary>
        /// Motivo pela qual foi cancelada, avaliações do médico etc
        /// </summary>
        [Column(TypeName = "ntext")]
        public virtual string ComentarioPaciente { get; set; }

        [Required]
        public virtual Medico MedicoResponsavel { get; set; }

        [Required]
        public virtual Paciente Paciente { get; set; }

        [Required]
        [EnumDataType(typeof(StatusConsulta))]
        public virtual StatusConsulta Status { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}