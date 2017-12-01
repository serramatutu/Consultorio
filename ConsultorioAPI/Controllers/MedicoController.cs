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

        public class IdDiagnosticoConsulta
        {
            public IdDiagnosticoConsulta()
            { }

            public Guid IdConsulta { get; set; }
            
            public string Diagnostico { get; set; }

            public bool PacientePresente { get; set; }
        }

        [Route("finalizarconsulta")]
        [HttpPost]
        public async Task<IHttpActionResult> FinalizarConsulta([FromBody]IdDiagnosticoConsulta param)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ResultadoOperacao r = await _consultaRepo.FinalizarConsulta(param.PacientePresente, param.Diagnostico, param.IdConsulta, GetUsuarioAtual().Id);
            return GetErrorResult(r);
        }

        [Route("getconsultaatual")]
        [HttpGet]
        public async Task<IHttpActionResult> GetConsultaAtual()
        {
            return Ok(_medicoRepo.GetConsultaAtual());
        }

        [Route("agenda")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAgenda()
        {
            var consultas = _consultaRepo.GetConsultasDeMedico(GetUsuarioAtual().Id).OrderBy(x => x.DataHora);
            return Ok(consultas);
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