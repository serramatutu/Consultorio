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
    [RoutePrefix("paciente")]
    [Authorize(Roles = "paciente")]
    public class PacienteController : BaseConsultorioController
    {
        ConsultaRepository _consultaRepo = new ConsultaRepository(new ConsultorioDbContext());
        PacienteRepository _pacienteRepo = new PacienteRepository(new ConsultorioDbContext());

        [Route("agenda")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAgenda()
        {
            var consultas = _consultaRepo.GetConsultasDeUsuario(GetUsuarioAtual().Id)
                                         .OrderBy(x => x.DataHora);
            return Ok(consultas);
        }

        [Route("agendarconsulta")]
        [HttpPost]
        public async Task<IHttpActionResult> AgendarConsulta([FromBody]AgendamentoConsulta agendamento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _consultaRepo.AgendarConsulta(agendamento,
                            GetUsuarioAtual().Id);

            return GetErrorResult(resultado);
        }

        [Route("cancelarconsulta")]
        [HttpPost]
        public async Task<IHttpActionResult> CancelarConsulta([FromBody]Guid idConsulta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ResultadoOperacao resultado = await _consultaRepo.CancelarConsulta(idConsulta, GetUsuarioAtual().Id);

            return GetErrorResult(resultado);
        }

        public class IdConsultaAvaliacaoConsulta
        {
            public IdConsultaAvaliacaoConsulta()
            { }
            public Guid IdConsulta { get; set; }
            public AvaliacaoConsulta Avaliacao { get; set; }
        }

        [Route("avaliarconsulta")]
        [HttpPost]
        public async Task<IHttpActionResult> AvaliarConsulta([FromBody]IdConsultaAvaliacaoConsulta param)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ResultadoOperacao resultado = await _consultaRepo.AvaliarConsulta(param.Avaliacao, param.IdConsulta, GetUsuarioAtual().Id);

            return GetErrorResult(resultado);
        }

        protected override void Dispose(bool disposing)
        {
            _consultaRepo.Dispose();
            _pacienteRepo.Dispose();
            base.Dispose(disposing);
        }

        #region Conta

        [Route("alterar/nome")]
        [HttpPost]
        public async Task<IHttpActionResult> AlterarNome([FromBody]string nome)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ResultadoOperacao resultado = _pacienteRepo.AlterarNome(User.Identity.Name, nome);

            return GetErrorResult(resultado);
        }

        [Route("alterar/endereco")]
        [HttpPost]
        public async Task<IHttpActionResult> AlterarEndereco([FromBody]string endereco)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ResultadoOperacao resultado = _pacienteRepo.AlterarEndereco(User.Identity.Name, endereco);

            return GetErrorResult(resultado);
        }

        [Route("alterar/datanasc")]
        [HttpPost]
        public async Task<IHttpActionResult> AlterarNome([FromBody]DateTime dataNasc)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ResultadoOperacao resultado = _pacienteRepo.AlterarDataNasc(User.Identity.Name, dataNasc);

            return GetErrorResult(resultado);
        }

        [Route("alterar/datanasc")]
        [HttpPost]
        public async Task<IHttpActionResult> AlterarTelefone([FromBody]string tel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ResultadoOperacao resultado = _pacienteRepo.AlterarTelefone(User.Identity.Name, tel);

            return GetErrorResult(resultado);
        }

        #endregion

        protected Paciente GetUsuarioAtual()
        {
            return _pacienteRepo.GetPacienteFromUsername(User.Identity.Name);
        }
    }
}