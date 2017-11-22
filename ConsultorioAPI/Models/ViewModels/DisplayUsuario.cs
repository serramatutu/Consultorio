using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ConsultorioAPI.Models.ViewModels
{
    public class DisplayUsuario
    {
        public DisplayUsuario(LoginUsuario u)
        {
            UserName = u.UserName;
            Email = u.Email;
            Papeis = u.Papeis.Select(p => p.Nome).ToArray();
        }

        /// <summary>
        /// Username do usuário
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Email do usuário
        /// </summary>
        [MaxLength(35)]
        [Required]
        public virtual string Email { get; set; }

        public virtual string[] Papeis { get; private set; }
    }
}