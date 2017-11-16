using ConsultorioAPI.Database;
using ConsultorioAPI.Database.Repositories;
using ConsultorioAPI.Models;
using ConsultorioAPI.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ConsultorioAPI.Controllers
{
    [RoutePrefix("paciente")]
    [EnableCors(Globals.CLIENT_URL,
                    "*",
                    "POST, GET",
                    SupportsCredentials = false)]
    //[Authorize(Roles = "paciente")]
    [Authorize(Roles = "paciente")]
    public class PacienteController : BaseConsultorioController
    {
        ConsultaRepository _consultaRepo = new ConsultaRepository(new ConsultorioDbContext());
        PacienteRepository _pacienteRepo = new PacienteRepository(new ConsultorioDbContext());

        [Route("agenda")]
        public async Task<IHttpActionResult> GetAgenda()
        {
            var consultas = _consultaRepo.GetConsultasDeUsuario(GetUsuarioAtual().Id).OrderBy(x => x.DataHora);
            return Ok(consultas.Select(x => new DisplayConsulta(x)));
        }

        [Route("agendarconsulta")]
        public async Task<IHttpActionResult> AgendarConsulta([FromBody]AgendamentoConsulta agendamento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _consultaRepo.AgendarConsulta(agendamento,
                            GetUsuarioAtual().Id);

            return GetErrorResult(resultado);
        }

        [Route("cancelarconsulta")]
        public async Task<IHttpActionResult> CancelarConsulta([FromBody]Guid idConsulta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ResultadoOperacao resultado = await _consultaRepo.CancelarConsulta(idConsulta, GetUsuarioAtual().Id);

            return GetErrorResult(resultado);
        }

        protected override void Dispose(bool disposing)
        {
            _consultaRepo.Dispose();
            _pacienteRepo.Dispose();
            base.Dispose(disposing);
        }

        protected Paciente GetUsuarioAtual()
        {
            return _pacienteRepo.GetPacienteFromUsername(User.Identity.Name);
        }
    }
}