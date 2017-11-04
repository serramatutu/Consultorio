using ConsultorioAPI.Models;
using System;
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
    [Authorize(Roles = "paciente")]
    public class PacienteController : ApiController
    {
        [Route("agendarconsulta")]
        public async Task<IHttpActionResult> AgendarConsulta([FromBody]AgendamentoConsulta consulta)
        {
            throw new NotImplementedException();
        }
    }
}