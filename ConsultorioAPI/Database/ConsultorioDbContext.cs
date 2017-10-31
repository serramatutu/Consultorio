﻿using ConsultorioAPI.Models;
using System.Data.Entity;

namespace ConsultorioAPI.Database
{
    public class ConsultorioDbContext : DbContext
    {
        public ConsultorioDbContext(string connection = null) : base(connection)
        {
            System.Data.Entity.Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<ConsultorioDbContext>()
            );
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsultorioUser>().ToTable("Usuario"); // Always ignore EntityAspect
        }

        public virtual IDbSet<ConsultorioUser> Users { get; set; }
    }
}