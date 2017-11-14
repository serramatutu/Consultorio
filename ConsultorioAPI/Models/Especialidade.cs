using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models
{
    [Table("Especialidade")]
    public class Especialidade
    {
        public Especialidade()
        { }

        public Especialidade(string nome)
        {
            Nome = nome;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar"), MaxLength(20)]
        public virtual string Nome { get; set; }
    }
}