using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
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
        { }

        /// <summary>
        /// Id do usuário
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Username do usuário
        /// </summary>
        [Column(TypeName = "nvarchar"), MaxLength(20)]
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Email do usuário
        /// </summary>
        [Column(TypeName = "varchar"), MaxLength(35)]
        [Required]
        public virtual string Email { get; set; }

        /// <summary>
        /// Hash da senha do usuário
        /// </summary>
        [Column(TypeName = "char"), MaxLength(68)]
        [Required]
        [JsonIgnore]
        public virtual string HashSenha { get; set; }

        /// <summary>
        /// Um valor aleatório que representa as credenciais do usuário. 
        /// É modificado sempre que suas credenciais mudam também
        /// </summary>
        [Column(TypeName = "varchar"), MaxLength(64)]
        [Required]
        [JsonIgnore]
        public virtual string SecurityStamp { get; set; }

        public virtual ICollection<PapelUsuario> Papeis { get; private set; }
    }

    public class PapelUsuario
    {
        public PapelUsuario()
        { }

        public PapelUsuario(string name)
        {
            Nome = name;
        }

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar"), MaxLength(30)]
        public string Nome { get; set; }

        public virtual ICollection<LoginUsuario> Usuarios { get; set; }
    }
}