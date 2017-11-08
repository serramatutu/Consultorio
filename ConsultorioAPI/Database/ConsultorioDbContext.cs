using ConsultorioAPI.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace ConsultorioAPI.Database
{
    public class ConsultorioDbContext : DbContext
    {
        static ConsultorioDbContext()
        {
            System.Data.Entity.Database.SetInitializer(
                new ConsultorioDbInitializer() // NÃO MEXA NISSO PELO AMOR DE JESUS CRISTO
            );
            //System.Data.Entity.Database.SetInitializer(
            //    new MigrateDatabaseToLatestVersion<ConsultorioDbContext, ConsultorioDbConfig>() // NÃO MEXA NISSO PELO AMOR DE JESUS CRISTO
            //);
        }

        public ConsultorioDbContext() : base("ConexaoBD")
        { }

        public ConsultorioDbContext(string connection = null) : base(connection)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginUsuario>().ToTable("Usuario");
            //modelBuilder.Entity<Medico>().ToTable("Medico");
            //modelBuilder.Entity<Paciente>().ToTable("Paciente");
            //modelBuilder.Entity<Consulta>().ToTable("Consulta");
            //modelBuilder.Entity<Especialidade>().ToTable("Especialidade");
            modelBuilder.Entity<PapelUsuario>().ToTable("Papel");

            modelBuilder.Entity<LoginUsuario>().HasMany(u => u.Papeis).WithMany(r => r.Usuarios).Map(m =>
            {
                m.ToTable("UsuarioPapel");
                m.MapLeftKey("IdUsuario");
                m.MapRightKey("IdPapel");
            });
        }

        public virtual DbSet<LoginUsuario> Usuarios { get; set; }

        //public virtual DbSet<Medico> Medicos { get; set; }

        //public virtual DbSet<Paciente> Pacientes { get; set; }

        public virtual DbSet<PapelUsuario> Papeis { get; set; }

        //public virtual DbSet<Consulta> Consultas { get; set; }

        //public virtual DbSet<Especialidade> Especialidades { get; set; }

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

    internal sealed class ConsultorioDbInitializer : CreateDatabaseIfNotExists<ConsultorioDbContext>
    {
        protected override void Seed(ConsultorioDbContext ctx)
        {
            ctx.Papeis.Add(new PapelUsuario("paciente"));
            ctx.Papeis.Add(new PapelUsuario("medico"));
            ctx.Papeis.Add(new PapelUsuario("admin"));
        }
    }
}