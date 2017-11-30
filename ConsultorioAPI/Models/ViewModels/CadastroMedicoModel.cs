using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models.ViewModels
{
    public class CadastroMedicoModel
    {
        [Required]
        [Display(Name = "usuário")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "a {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "confirmação de senha")]
        [Compare("Senha", ErrorMessage = "As senha e a confirmação de senha devem ser idênticas.")]
        public string ConfSenha { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "nome completo")]
        public string NomeCompleto { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "data de nascimento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataNasc { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "telefone")]
        public string Telefone { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "celular")]
        public string Celular { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "endereco")]
        public string Endereco { get; set; }

        [Required]
        [MaxLength(7)]
        [Display(Name = "CRM")]
        public string CRM { get; set; }

        [Required]
        public int IdEspecialidade { get; set; }
    }
}