using System;
using System.ComponentModel.DataAnnotations;

namespace ConsultorioAPI.Models.ViewModels
{
    public class DisplayPaciente
    {
        public DisplayPaciente(Paciente p)
        {
            Nome = p.Nome;
            Telefone = p.Telefone;
            DataNasc = p.DataNasc;
            Endereco = p.Endereco;
        }

        [Required]
        [MaxLength(40)]
        public virtual string Nome { get; set; }

        [MaxLength(14)]
        [Required]
        public virtual string Telefone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual DateTime DataNasc { get; set; }

        //public virtual Bitmap Foto { get; set; }

        [MaxLength(50)]
        [Required]
        public virtual string Endereco { get; set; }
    }
}