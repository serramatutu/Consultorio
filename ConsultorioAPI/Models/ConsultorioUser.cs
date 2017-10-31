using Microsoft.AspNet.Identity;
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
        /// Se o email está confirmado
        /// </summary>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        /// Hash da senha do usuário
        /// </summary>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        /// Um valor aleatório que representa as credenciais do usuário. 
        /// É modificado sempre que suas credenciais mudam também
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        /// Telefone do usuário
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Se o telefone foi confirmado
        /// </summary>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Se a autenticação de dois fatores (ex: token no celular) está habilitada
        /// </summary>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Caso o usuário esteja bloqueado do sistema (por exemplo, por muitas tentativas inválidas de logon),
        /// armazena até quando ele estará bloqueado.
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// Se ele está bloqueado
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        /// O número de vezes que o login falhou
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        public virtual List<IdentityUserClaim> Claims { get; private set; }
    }

    public class IdentityUserClaim
    {
        public virtual string Id { get; set; }
        public virtual string UserId { get; set; }
        public virtual string ClaimType { get; set; }
        public virtual string ClaimValue { get; set; }
    }
}