using ConsultorioAPI.Database;
using ConsultorioAPI.Database.Repositories;
using ConsultorioAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ConsultorioAPI.Controllers
{
    [RoutePrefix("medico")]
    [EnableCors(Globals.CLIENT_URL,
                    "*",
                    "POST, GET",
                    SupportsCredentials = false)]
    [Authorize(Roles = "medico")]
    public class MedicoController : ApiController
    {
        ConsultaRepository _consultaRepo = new ConsultaRepository(new ConsultorioDbContext());

        [Route("comentarconsulta")]
        public async Task<IHttpActionResult> ComentarConsulta([FromBody]Guid idConsulta, [FromBody]string comentario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ResultadoOperacao r = await _consultaRepo.ComentarConsulta(comentario, idConsulta);
            return GetErrorResult(r);
        }

        protected IHttpActionResult GetErrorResult(ResultadoOperacao r)
        {
            if (r == null || r.ErroInterno)
                return InternalServerError();

            if (r.Sucesso)
                return Ok();

            return BadRequest(r.Mensagem);
        }

        protected override void Dispose(bool disposing)
        {
            _consultaRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}