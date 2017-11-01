using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ConsultorioAPI.Models
{
    [Table("Usuarios")]
    public class ConsultorioUser : IUser
    {
        public ConsultorioUser()
        {

        }

        /// <summary>
        /// Id do usuário
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Username do usuário
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email do usuário
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Hash da senha do usuário
        /// </summary>
        public virtual string HashSenha { get; set; }

        /// <summary>
        /// Um valor aleatório que representa as credenciais do usuário. 
        /// É modificado sempre que suas credenciais mudam também
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        /// Telefone do usuário
        /// </summary>
        public virtual string Telefone { get; set; }

        public virtual List<UserRole> Roles { get; private set; }

        public virtual List<IdentityUserClaim> Claims { get; private set; }
    }

    public class UserRole
    {
        public UserRole(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}