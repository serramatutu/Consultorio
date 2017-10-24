using ConsultorioAPI.Database.Contexts;
using ConsultorioAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Database.Initializers
{
    public class AuthDbInitializer : DropCreateDatabaseAlways<AuthContext>
    {
        protected override void Seed(AuthContext context)
        {
            var usermanager = new UserManager<UserModel>(new UserStore<UserModel>(context));
        }
    }
}