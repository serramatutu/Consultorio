using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioAPI.Database.Repositories
{
    public class ConsultorioBaseRepository : IDisposable
    {
        protected ConsultorioDbContext _ctx;

        public ConsultorioBaseRepository(ConsultorioDbContext ctx)
        {
            _ctx = ctx;
        }

        protected bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    _ctx.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}