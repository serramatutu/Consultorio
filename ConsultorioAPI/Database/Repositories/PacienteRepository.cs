using ConsultorioAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Database.Repositories
{
    public class PacienteRepository : IDisposable
    {
        ConsultorioDbContext _ctx;

        public PacienteRepository(ConsultorioDbContext ctx)
        {
            _ctx = ctx;
        }

        public Paciente GetPacienteFromUsername(string username)
        {
            LoginUsuario loginUsuario = _ctx.Usuarios.FirstOrDefault(x => x.UserName == username);
            if (loginUsuario == null)
                return null;

            return _ctx.Pacientes.FirstOrDefault(p => p.DadosLogin.Id.Equals(loginUsuario.Id));
        }

        protected bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}