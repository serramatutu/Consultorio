﻿using ConsultorioAPI.Database;
using ConsultorioAPI.Database.Repositories;
using ConsultorioAPI.Models;
using ConsultorioAPI.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace ConsultorioAPI.Controllers
{
    [RoutePrefix("paciente")]
    [EnableCors(Globals.CLIENT_URL,
                    "*",
                    "POST, GET",
                    SupportsCredentials = false)]
    [Authorize(Roles = "paciente")]
    public class PacienteController : ApiController
    {
        ConsultaRepository _consultaRepo = new ConsultaRepository(new ConsultorioDbContext());
        PacienteRepository _pacienteRepo = new PacienteRepository(new ConsultorioDbContext());

        [Route("agendarconsulta")]
        public async Task<IHttpActionResult> AgendarConsulta([FromBody]AgendamentoConsulta agendamento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _consultaRepo.AgendarConsulta(agendamento,
                            _pacienteRepo.GetPacienteFromUsername(User.Identity.Name));

            return GetErrorResult(resultado);
        }

        [Route("cancelarconsulta")]
        public async Task<IHttpActionResult> CancelarConsulta([FromBody]Guid idConsulta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ResultadoOperacao resultado = await _consultaRepo.CancelarConsulta(idConsulta);

            return GetErrorResult(resultado);
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
            _pacienteRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}