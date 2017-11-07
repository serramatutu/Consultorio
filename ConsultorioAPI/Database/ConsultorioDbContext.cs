﻿using ConsultorioAPI.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultorioAPI.Database
{
    public class ConsultorioDbContext : DbContext
    {
        public ConsultorioDbContext(string connection = null) : base(connection)
        {
            System.Data.Entity.Database.SetInitializer(
                new CreateDatabaseIfNotExists<ConsultorioDbContext>() // NÃO MEXA NISSO PELO AMOR DE JESUS CRISTO
            );
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginUsuario>().ToTable("Usuario");
            modelBuilder.Entity<PapelUsuario>().ToTable("Papel");

            modelBuilder.Entity<LoginUsuario>().HasMany(u => u.Papeis).WithMany(r => r.Usuarios).Map(m =>
            {
                m.ToTable("UsuarioPapel");
                m.MapLeftKey("IdUsuario");
                m.MapRightKey("IdPapel");
            });
        }

        public virtual DbSet<LoginUsuario> Usuarios { get; set; }

        public virtual DbSet<Medico> Medicos { get; set; }

        public virtual DbSet<Paciente> Pacientes { get; set; }

        public virtual DbSet<PapelUsuario> Papeis { get; set; }

        public virtual DbSet<Consulta> Consultas { get; set; }

        public virtual DbSet<Especialidade> Especialidades { get; set; }

        /// <summary>
        /// Erros decentes por favor ne
        /// </summary>
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw MelhorarExcecao(ex);
            }
        }

        /// <summary>
        /// Erros decentes por favor ne
        /// </summary>
        public async override Task<int> SaveChangesAsync()
        {
            try
            {
                return await base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                throw MelhorarExcecao(ex);
            }
        }

        public DbEntityValidationException MelhorarExcecao(DbEntityValidationException ex)
        {
            var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

            var msgCompleta = string.Join("; ", errorMessages);

            var msgExcecao = string.Concat(ex.Message, " Erros : ", msgCompleta);

            throw new DbEntityValidationException(msgExcecao, ex.EntityValidationErrors);
        }
    }
}