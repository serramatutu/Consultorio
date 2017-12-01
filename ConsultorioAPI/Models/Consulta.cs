using ConsultorioAPI.Models.ViewModels;
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
        public virtual DateTime DataHora { get; set; }

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
        public virtual string Diagnostico { get; set; }

        /// <summary>
        /// Avaliação dada pelo paciente ao serviço prestado
        /// </summary>
        public virtual AvaliacaoConsulta Avaliacao { get; set; } = new AvaliacaoConsulta();

        [Required]
        public virtual Medico Medico { get; set; }

        [Required]
        public virtual Paciente Paciente { get; set; }

        [EnumDataType(typeof(StatusConsulta))]
        public virtual StatusConsulta Status { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}