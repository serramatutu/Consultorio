using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.ModelBinding;

namespace ConsultorioAPI.Models
{
    [Table("Usuario")]
    public class LoginUsuario : IUser<Guid>
    {
        public LoginUsuario()
        {

        }

        /// <summary>
        /// Id do usuário
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Username do usuário
        /// </summary>
        [Column(TypeName = "nvarchar"), MaxLength(20)]
        public string UserName { get; set; }

        /// <summary>
        /// Email do usuário
        /// </summary>
        [Column(TypeName = "varchar"), MaxLength(35)]
        public virtual string Email { get; set; }

        /// <summary>
        /// Hash da senha do usuário
        /// </summary>
        [Column(TypeName = "char"), MaxLength(68)]
        public virtual string HashSenha { get; set; }

        /// <summary>
        /// Um valor aleatório que representa as credenciais do usuário. 
        /// É modificado sempre que suas credenciais mudam também
        /// </summary>
        [Column(TypeName = "varchar"), MaxLength(64)]
        public virtual string SecurityStamp { get; set; }

        public virtual List<PapelUsuario> Papeis { get; private set; }

        public virtual List<IdentityUserClaim> Claims { get; private set; }
    }

    public class PapelUsuario
    {
        public PapelUsuario(string name)
        {
            Nome = name;
        }

        public int Id { get; set; }

        [Column(TypeName = "nvarchar"), MaxLength(30)]
        public string Nome { get; set; }

        public virtual List<LoginUsuario> Usuarios { get; set; }
    }
}