using ConsultorioAPI.Database;
using ConsultorioAPI.Database.Repositories;
using ConsultorioAPI.Models;
using ConsultorioAPI.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConsultorioAPI.Controllers
{
    [RoutePrefix("medico")]
    [Authorize(Roles = "medico")]
    public class MedicoController : BaseConsultorioController
    {
        ConsultaRepository _consultaRepo = new ConsultaRepository(new ConsultorioDbContext());
        MedicoRepository _medicoRepo = new MedicoRepository(new ConsultorioDbContext());

        [Route("comentarconsulta")]
        [HttpPost]
        public async Task<IHttpActionResult> ComentarConsulta([FromBody]Guid idConsulta, [FromBody]string comentario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ResultadoOperacao r = await _consultaRepo.ComentarConsultaMedico(comentario, idConsulta);
            return GetErrorResult(r);
        }

        [Route("agenda")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAgenda()
        {
            var consultas = _consultaRepo.GetConsultasDeMedico(GetUsuarioAtual().Id).OrderBy(x => x.DataHora);
            return Ok(consultas.Select(x => new DisplayConsulta(x)));
        }

        public Medico GetUsuarioAtual()
        {
            return _medicoRepo.GetMedico(User.Identity.Name);
        }

        protected override void Dispose(bool disposing)
        {
            _consultaRepo.Dispose();
            _medicoRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}