using ConsultorioAPI.Database;
using ConsultorioAPI.Database.Repositories;
using ConsultorioAPI.Models;
using ConsultorioAPI.Models.ViewModels;
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
    public class MedicoController : BaseConsultorioController<Medico>
    {
        ConsultaRepository _consultaRepo = new ConsultaRepository(new ConsultorioDbContext());
        MedicoRepository _medicoRepo = new MedicoRepository(new ConsultorioDbContext());

        [Route("comentarconsulta")]
        public async Task<IHttpActionResult> ComentarConsulta([FromBody]Guid idConsulta, [FromBody]string comentario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ResultadoOperacao r = await _consultaRepo.ComentarConsulta(comentario, idConsulta);
            return GetErrorResult(r);
        }

        [Route("agenda")]
        public async Task<IHttpActionResult> GetAgenda()
        {
            var consultas = _consultaRepo.GetConsultasDeMedico(GetUsuarioAtual().Id).OrderBy(x => x.DataHora);
            return Ok(consultas.Select(x => new DisplayConsulta(x)));
        }

        protected override void Dispose(bool disposing)
        {
            _consultaRepo.Dispose();
            _medicoRepo.Dispose();
            base.Dispose(disposing);
        }

        protected override Medico GetUsuarioAtual()
        {
            throw new NotImplementedException();
        }
    }
}