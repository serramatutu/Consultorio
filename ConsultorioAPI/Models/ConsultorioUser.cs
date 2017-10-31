﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Models
{
    public class ConsultorioUser : IUser
    {
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
    }

    public class UserRole
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}